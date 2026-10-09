Highcharts.setOptions({
    lang: {
        contextButtonTitle: 'Options for Saving/Using'
    }
});

SetupRunGraph = function (id, variable, title, values) {
    $('#container' + id).highcharts({
        chart: {
            zoomType: 'xy',
            panning: true,
            panKey: 'shift',
            borderWidth: 3,
            borderRadius: 15,
            spacingLeft: 75
        },
        title: {

            style: {
                fontSize: '20px',
                fontWeight: 'bold'
            },

            text: title
        },

        credits: {
            enabled: false
        },

        legend: {
            enabled: false
        },

        xAxis: {
            type: 'category',
            title: {
                text: ''
            }
        },

        yAxis: {
            title: {

                text: ''
            }
        },

        series: [

            {
                name: variable,
                color: 'Blue',
                data: [
                    
                ]
            }

        ]

    }, function (chart) {

        chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)
            .on('click', function () {
                location.href = 'https://korbel.du.edu/pardee/';
            })
            .attr({
                zIndex: 7
            })
            .css({
                cursor: 'pointer'
            })
            .add();

        var myWidth = 0, myHeight = 0;
        if (typeof (window.innerWidth) == 'number') {
            //Non-IE
            myWidth = window.innerWidth;
            myHeight = window.innerHeight;
        } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
            //IE 6+ in 'standards compliant mode'
            myWidth = document.documentElement.clientWidth;
            myHeight = document.documentElement.clientHeight;
        } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
            //IE 4 compatible
            myWidth = document.body.clientWidth;
            myHeight = document.body.clientHeight;
        }
        if (myWidth == 0) {
            myWidth = screen.width;
            myHeight = screen.height;
        }

        myWidth = myWidth * 0.95 - 50;
        myHeight = myHeight * 0.90 - 50;

        myWidth = myWidth * 0.49;
        myHeight = myHeight * 0.49;

        chart.setSize(myWidth, myHeight);
        chart.hasUserSize = null;

    });
}

const RunStatus = {
    NOT_STARTED: 0,
    UNEXPECTED: 1,
    WAITING_PROGRESS_FILE: 2,
    NO_PROGRESS_FILE: 3,
    ERROR_FILE: 4,
    COMPLETED: 5
}