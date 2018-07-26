var page = require('webpage').create();
page.onConsoleMessage = function (msg) {
    console.log(msg);
}
system = require('system');
var urlchart;
var path;
var path2;
var language;
var domain;
if (system.args.length == 1) {
    phantom.exit();
}
else {
    urlchart = system.args[1];
    path = system.args[2];
    path2 = system.args[3];
    language = system.args[4];
    domain = system.args[5];
}

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

var width = 2000;
var height = 10000;
page.viewportSize = { width: width, height: height }; //浏览器大小，宽度根据网页情况自行设置，高度可以随意，因为后面会滚动到底部

//console.log("srart urlchart" + urlchart);
var page2 = require('webpage').create();
page2.viewportSize = { width: width, height: height }; //浏览器大小，宽度根据网页情况自行设置，高度可以随意，因为后面会滚动到底部

page2.open(urlchart, function (status) {
    if (status != "success") {
        console.log('FAIL to load the address2');
        phantom.exit();
    }
    var chartNetwork,chartNetwork2;
    window.setTimeout(function () {
        chartNetwork = page2.evaluate(function () {
            //此函数在目标页面执行的，上下文环境非本phantomjs，所以不能用到这个js中其他变量
            window.scrollTo(0, 10000);//滚动到底部
            var div = document.getElementById('net'); //要截图的div的id
            var bc = div.getBoundingClientRect();
            var top = bc.top;
            var left = bc.left;
            var width = bc.width;
            var height = bc.height;
            return [top, left, width, height];
            //return document.getElementsByTagName('html')[0].getBoundingClientRect();
        });
        console.log(chartNetwork);
        page2.clipRect = { //截图的偏移和宽高
            top: chartNetwork[0],
            left: chartNetwork[1],
            width: chartNetwork[2],
            height: chartNetwork[3]
        };
        page2.render(path);
        

        chartNetwork2 = page2.evaluate(function () {
            //此函数在目标页面执行的，上下文环境非本phantomjs，所以不能用到这个js中其他变量
            window.scrollTo(0, 10000);//滚动到底部
            var div = document.getElementById('net2'); //要截图的div的id
            console.log(div);
            var bc = div.getBoundingClientRect();
            var top = bc.top;
            var left = bc.left;
            var width = bc.width;
            var height = bc.height;
            return [top, left, width, height];
            //return document.getElementsByTagName('html')[0].getBoundingClientRect();
        });
        console.log(chartNetwork2);
        page2.clipRect = { //截图的偏移和宽高
            top: chartNetwork2[0],
            left: chartNetwork2[1],
            width: chartNetwork2[2],
            height: chartNetwork2[3]
        };
        page2.render(path2);
        phantom.exit();
    }, 2500);

});