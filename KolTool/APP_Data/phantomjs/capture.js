var page = require('webpage').create();
page.onConsoleMessage = function (msg) {
    console.log(msg);
}
system = require('system');
var url;
var path;
var path2;
var path3;
var language;
var domain;
if (system.args.length == 1) {
    phantom.exit();
}
else {
    url = system.args[1];
    path = system.args[2];
    path2 = system.args[3];
    path3 = system.args[4];
    path4 = system.args[5];
    path5 = system.args[6];
    path6 = system.args[7];
    path7 = system.args[8];
    language = system.args[9];
    domain = system.args[10];
}
var width = 2000;
var height = 10000;
page.viewportSize = { width: width, height: height }; //浏览器大小，宽度根据网页情况自行设置，高度可以随意，因为后面会滚动到底部


if (language == 'en') {
    phantom.addCookie({
        name: 'lang',
        value: 'en-US',
        domain: domain,
        path: '/',
        secure: false,
        httponly: false,
        expires: Date.now() + (1000 * 60 * 60 * 24 * 5)
    });
} else {
    phantom.addCookie({
        name: 'lang',
        value: 'zh-CN',
        domain: domain,
        path: '/',
        secure: false,
        httponly: false,
        expires: Date.now() + (1000 * 60 * 60 * 24 * 5)
    });
}


//console.log("srart url" + url);
page.open(url, function (status) {
    if (status != "success") {
        console.log('FAIL to load the address');
        phantom.exit();
    }
    var chart1, chart2, chart3, chart4, chart5, chart6, chart7;
    window.setTimeout(function () {
        chart1 = page.evaluate(function () {
            //此函数在目标页面执行的，上下文环境非本phantomjs，所以不能用到这个js中其他变量
            window.scrollTo(0, 10000);//滚动到底部
            var div = document.getElementById('visualization'); //要截图的div的id
            var bc = div.getBoundingClientRect();

            var top = bc.top;
            console.log(top);
            var left = bc.left;
            var width = bc.width;
            var height = bc.height;
            return [top, left, width, height];
        });
        console.log(chart1);
        page.clipRect = { //截图的偏移和宽高
            top: chart1[0],
            left: chart1[1],
            width: chart1[2],
            height: chart1[3]
        };

        page.render(path);

        chart2 = page.evaluate(function () {
            //此函数在目标页面执行的，上下文环境非本phantomjs，所以不能用到这个js中其他变量
            window.scrollTo(0, 10000);//滚动到底部
            var div = document.getElementById('visualization2'); //要截图的div的id
            var bc = div.getBoundingClientRect();
            var top = bc.top;
            var left = bc.left;
            var width = bc.width;
            var height = bc.height;
            return [top, left, width, height];
        });
        page.clipRect = { //截图的偏移和宽高
            top: chart2[0],
            left: chart2[1],
            width: chart2[2],
            height: chart2[3]
        };
        console.log(chart2);
        page.render(path2);

        chart3 = page.evaluate(function () {
            //此函数在目标页面执行的，上下文环境非本phantomjs，所以不能用到这个js中其他变量
            window.scrollTo(0, 10000);//滚动到底部
            var div = document.getElementById('visualization3'); //要截图的div的id
            var bc = div.getBoundingClientRect();
            var top = bc.top;
            var left = bc.left;
            var width = bc.width;
            var height = bc.height;
            return [top, left, width, height];
        });
        page.clipRect = { //截图的偏移和宽高
            top: chart3[0],
            left: chart3[1],
            width: chart3[2],
            height: chart3[3]
        };
        console.log(chart3);
        page.render(path3);


        chart4 = page.evaluate(function () {
            //此函数在目标页面执行的，上下文环境非本phantomjs，所以不能用到这个js中其他变量
            window.scrollTo(0, 10000);//滚动到底部
            var div = document.getElementById('visualization4'); //要截图的div的id
            var bc = div.getBoundingClientRect();
            var top = bc.top;
            var left = bc.left;
            var width = bc.width;
            var height = bc.height;
            return [top, left, width, height];
        });
        page.clipRect = { //截图的偏移和宽高
            top: chart4[0],
            left: chart4[1],
            width: chart4[2],
            height: chart4[3]
        };
        console.log(chart4);
        page.render(path4);

        chart5 = page.evaluate(function () {
            //此函数在目标页面执行的，上下文环境非本phantomjs，所以不能用到这个js中其他变量
            window.scrollTo(0, 10000);//滚动到底部
            var div = document.getElementById('visualization5'); //要截图的div的id
            var bc = div.getBoundingClientRect();
            var top = bc.top;
            var left = bc.left;
            var width = bc.width;
            var height = bc.height;
            return [top, left, width, height];
        });
        page.clipRect = { //截图的偏移和宽高
            top: chart5[0],
            left: chart5[1],
            width: chart5[2],
            height: chart5[3]
        };
        console.log(chart5);
        page.render(path5);


        chart6 = page.evaluate(function () {
            //此函数在目标页面执行的，上下文环境非本phantomjs，所以不能用到这个js中其他变量
            window.scrollTo(0, 10000);//滚动到底部
            var div = document.getElementById('net'); //要截图的div的id
            var bc = div.getBoundingClientRect();
            var top = bc.top;
            var left = bc.left;
            var width = bc.width;
            var height = bc.height;
            return [top, left, width, height];
        });
        page.clipRect = { //截图的偏移和宽高
            top: chart6[0],
            left: chart6[1],
            width: chart6[2],
            height: chart6[3]
        };
        console.log(chart6);
        page.render(path6);


        chart7 = page.evaluate(function () {
            //此函数在目标页面执行的，上下文环境非本phantomjs，所以不能用到这个js中其他变量
            window.scrollTo(0, 10000);//滚动到底部
            var div = document.getElementById('net2'); //要截图的div的id
            var bc = div.getBoundingClientRect();
            var top = bc.top;
            var left = bc.left;
            var width = bc.width;
            var height = bc.height;
            return [top, left, width, height];
        });
        page.clipRect = { //截图的偏移和宽高
            top: chart7[0],
            left: chart7[1],
            width: chart7[2],
            height: chart7[3]
        };
        console.log(chart7);
        page.render(path7);
        phantom.exit();
    }, 2000);
});