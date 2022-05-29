//startday = new Date();
//clockStart = startday.getTime();

function initStopwatch() {
    var myTime = new Date();
    var timeNow = myTime.getTime();
    var timeDiff = timeNow - clockStart;
    return timeDiff / 1000;
}

function getSecs() {
    var mySecs = initStopwatch();
    var mySecs1 = "" + mySecs;
    mySecs1 = mySecs1.substring(0, mySecs1.indexOf(".")) + " secs.";
    document.form1.timespent.value = mySecs1
    window.setTimeout('getSecs()', 1000);
}