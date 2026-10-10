'use strict';
const $=id=>document.getElementById(id);
let state=null,shown=null,initialized=false,submitting=false;
async function api(path,body){const headers={'Content-Type':'application/json'};if(body)headers['X-Companion-Token']=parent.document.querySelector('meta[name=companion-token]')?.content||'';const response=await fetch(path,{method:body?'POST':'GET',headers,body:body?JSON.stringify(body):undefined});const result=await response.json();if(!response.ok)throw Error(result.error||'Request failed');return result;}
function notice(text,error=false){$('status').textContent=text;$('statusBar').classList.toggle('error',error);}
function notify(){parent.postMessage({type:'tool-navigation'},location.origin);}
function home(){shown=null;$('answerView').hidden=true;$('startView').hidden=false;notify();}
function selectionReady(){return state?.active && $('sourceKind').value===state.active.kind && (state.active.kind==='bundled'||JSON.stringify($('folders').value.split('\n').map(x=>x.trim()).filter(Boolean))===JSON.stringify(state.active.roots));}
function controls(){const busy=submitting||state?.job.status==='running';for(const id of ['prepare','sourceKind','folders','browse'])$(id).disabled=busy;$('ask').disabled=busy||!selectionReady()||!state?.ai.configured;$('search').disabled=busy||!selectionReady();$('semantic').disabled=busy||!selectionReady()||!state?.active?.research_version||state?.active?.semantic_ready;$('cancel').hidden=!busy;$('statusBar').classList.toggle('busy',busy);}
function sourceLabel(source){return source.kind==='bundled'?'Bundled source · '+source.revisions.map(r=>r.repository.split('/').pop()+' '+r.commit.slice(0,8)).join(' · '):'Local source · '+source.roots.join(' · ');}
async function showAnswer(id){const record=await api('api/answer?id='+encodeURIComponent(id));shown=id;$('startView').hidden=true;$('answerView').hidden=false;$('answerQuestion').textContent=record.question;$('answerMode').textContent=record.mode==='ask'?'CODE EXPLANATION':'LOCAL SOURCE SEARCH';$('answerSource').textContent=sourceLabel(record.source);$('answerText').replaceChildren();
 for(const part of record.answer.split(/(\[E\d+\])/g)){if(/^\[E\d+\]$/.test(part)){const b=document.createElement('button');b.className='citation';b.textContent=part;b.onclick=()=>{const el=$(part.slice(1,-1));if(el){el.open=true;el.scrollIntoView({behavior:'smooth',block:'center'});}};$('answerText').append(b);}else $('answerText').append(document.createTextNode(part));}
 $('researchSummary').textContent=[record.semantic_status,record.metrics?`${record.metrics.model_turns} AI turns · ${record.metrics.tool_calls} research steps`:null,...(record.unresolved||[])].filter(Boolean).join(' · ');
 $('evidence').replaceChildren();for(const e of record.evidence){const d=document.createElement('details');d.id=e.id;const s=document.createElement('summary');const p=document.createElement('p');p.className='muted';const pre=document.createElement('pre');pre.textContent=e.text;
 if(e.kind==='wiki'){s.textContent=`[${e.id}] ${e.title} · ${e.heading}`;p.textContent=`Offline wiki · revision ${e.revision} · fetched ${e.fetched_at} · selection: ${e.selection} · approval checked ${e.approval.checked_at}. ${e.freshness_notice}`;const link=document.createElement('a');try{const url=new URL(e.citation);if(url.protocol==='https:'){link.href=url.href;link.target='_blank';link.rel='noopener noreferrer';link.textContent='Open cited revision';p.append(' ',link);}}catch{}}
 else if(e.kind==='research'){s.textContent=`[${e.id}] ${e.tool}${e.successful?'':' · failed (not citable)'}`;p.textContent='Saved research result. Search and structural matches require source context; these are not compiler-resolved dependencies.';}
 else{s.textContent=`[${e.id}] ${e.path} · lines ${e.start}–${e.end}`;p.textContent=`Snapshot SHA256: ${e.sha256}${e.excerpt_truncated?' · Excerpt shortened.':''}`;}
 d.append(s,p,pre);$('evidence').append(d);}if(!record.evidence.length)$('evidence').textContent='No matching evidence.';notify();}

async function refresh(){state=await api('api/state');if(!initialized){if(state.active){$('sourceKind').value=state.active.kind;if(state.active.kind==='local')$('folders').value=state.active.roots.join('\n');}initialized=true;sourceChanged();}
 $('sourceBadge').textContent=state.active?`${state.active.indexed_files} files indexed`:'Not prepared';$('revision').textContent=state.active?sourceLabel(state.active):'Bundled: '+state.bundled.map(r=>r.repository.split('/').pop()+' '+r.commit.slice(0,8)).join(' · ');if(state.bundle_changed)$('revision').textContent+=' · A new bundle is available. Prepare the index to use it.';
 $('researchNote').textContent=state.active?.research_version?`Offline wiki: ${state.active.wiki_pages} pages · fetched ${(state.active.wiki_dates||[]).join(', ')} · ${state.active.semantic_ready?'Semantic search ready':'Exact/text search ready; semantic search can be prepared locally.'}`:'Prepare the index to enable code navigation and offline wiki retrieval.';
 $('aiNote').textContent=state.ai.configured?state.ai.model:'Set up AI in Companion Settings to ask questions.';controls();notice(state.job.message,state.job.status==='failed');
 $('history').replaceChildren();if(!state.history.length)$('history').textContent='Your answers and source evidence will appear here.';for(const item of state.history){const b=document.createElement('button');b.className='history-item';b.textContent=item.question;const small=document.createElement('small');small.textContent=(item.mode==='ask'?'Answer':'Search')+' · '+new Date(item.created).toLocaleString();b.append(small);b.onclick=()=>showAnswer(item.id).catch(e=>notice(e.message,true));$('history').append(b);}
 if(state.job.answer_id&&state.job.answer_id!==lastCompleted){lastCompleted=state.job.answer_id;await showAnswer(lastCompleted);}}
let lastCompleted=null;
function sourceChanged(){$('localSources').hidden=$('sourceKind').value!=='local';}
$('sourceKind').onchange=()=>{sourceChanged();controls();};$('folders').oninput=controls;
async function run(action,body){submitting=true;controls();try{await api('api/'+action,body);await refresh();}catch(e){notice(e.message,true);}finally{submitting=false;controls();}}
$('prepare').onclick=()=>run('index',{kind:$('sourceKind').value,roots:$('folders').value.split('\n').map(x=>x.trim()).filter(Boolean)});
$('semantic').onclick=()=>run('semantic',{});
$('ask').onclick=()=>run('ask',{question:$('question').value});$('search').onclick=()=>run('search',{question:$('question').value});
$('browse').onclick=async()=>{try{const result=await parent.companionTools.pickFolder($('folders').value.split('\n')[0]||'');if(result.installation){const folders=$('folders').value.split('\n').filter(Boolean);if(!folders.includes(result.installation))folders.push(result.installation);$('folders').value=folders.slice(0,4).join('\n');controls();}}catch(e){notice('Enter the source folder path directly, or update Companion to use Browse.',true);}};
$('aiSettings').onclick=()=>{if(parent.companionTools?.openAISettings)parent.companionTools.openAISettings();else notice('Update Companion to configure the shared AI connection.',true);};
async function stop(){await api('api/stop',{});await refresh();home();}
$('cancel').onclick=()=>stop().catch(e=>notice(e.message,true));$('back').onclick=home;
window.companionSession={canGoBack:()=>!$('answerView').hidden,back:home,stop};
async function poll(){try{await refresh();}catch(e){notice(e.message,true);}finally{setTimeout(poll,1200);}}
poll();
