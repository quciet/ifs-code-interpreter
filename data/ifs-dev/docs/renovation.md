# Renovating `Working.run`: Principle and Practice

Last revised: 2018-07-09. Author: LIU Yu

## Introduction

While making the design choices, we have considered and balance a nubmer of feature- and non-features requirements.

- structured, and relational modeling of variables and dimensions;
- self-contained and self-descriptive metadata management;
- reader-side forward and backward compatibility in the long term;
- availability of professional and voluntary support in the long term;

## Overview of Two-Tier Structure

*IFs/Vanilla* defines a two-layered storage structure for `Working.run`. Metadata about varaiables, dimensions, and configuration parameters are stored in [SQLite](https://www.nuget.org/packages/Microsoft.Data.SQLite/) database astables; then the *dataframes* of variables are stored in the database as opaque [BLOB](https://www.sqlite.org/datatype3.html). Internal to the *dataframes*, we use the [Parquet](https://www.nuget.org/packages/Parquet.Net/) columnar data storage format to manage the dimensions and metrics of variables. Query processing over *dataframes* can take advantage of multiple technologies defined by the [Parquet](https://github.com/apache/parquet-format) format, including row groups filtering, dictionary encoding, and [snappy](https://google.github.io/snappy/)- or [LZ4](https://lz4.github.io/lz4/)-based compression.

TODO

## Design of SQLite Database 

TODO

## Design of Parquet Dataframe

TODO

## Compatibility Guarantees

Compatibility guarantees are enumerated as follows,

| Operation | Direction | Description                                   | Support
|-----------|-----------|-----------------------------------------------|----------
| Read      | Backward  | New `IFs.Web` can query old `Working.run`     | Yes
| Read      | Forward   | Old `IFs.Web` can query new `Working.run`     | Yes
| Write     | Backward  | New `IFs.Cli` can upgrade old `Working.run` to a later set of variable / dimension metadata | No, but possible
| Write     | Forward   | Old `IFs.Cli` can downgrade new `Working.run` to an older set of variable / dimension metadata | No


## Benchmark

We performed a simple benchmark to measure the end-to-end performance of querying variables / dataframes against the new `Working.run` file.

```
castle:~ liuyu$ ab -n 1000 -c 1 "https://localhost:5001/api/query/EDBASENRG/2015,2050?sDim=China"
This is ApacheBench, Version 2.3 <$Revision: 1826891 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 100 requests
Completed 200 requests
Completed 300 requests
Completed 400 requests
Completed 500 requests
Completed 600 requests
Completed 700 requests
Completed 800 requests
Completed 900 requests
Completed 1000 requests
Finished 1000 requests


Server Software:        Kestrel
Server Hostname:        localhost
Server Port:            5001
SSL/TLS Protocol:       TLSv1.2,ECDHE-RSA-AES256-GCM-SHA384,2048,256
TLS Server Name:        localhost

Document Path:          /api/query/EDBASENRG/2015,2050?sDim=China
Document Length:        1916 bytes

Concurrency Level:      1
Time taken for tests:   170.107 seconds
Complete requests:      1000
Failed requests:        0
Total transferred:      2049000 bytes
HTML transferred:       1916000 bytes
Requests per second:    5.88 [#/sec] (mean)
Time per request:       170.107 [ms] (mean)
Time per request:       170.107 [ms] (mean, across all concurrent requests)
Transfer rate:          11.76 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:       49   51   4.6     51     174
Processing:   107  119  42.6    116    1449
Waiting:      107  118  42.4    116    1441
Total:        157  170  46.7    167    1622

Percentage of the requests served within a certain time (ms)
  50%    167
  66%    169
  75%    171
  80%    172
  90%    175
  95%    179
  98%    186
  99%    202
 100%   1622 (longest request)
```