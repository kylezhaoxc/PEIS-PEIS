jQuery(document).ready(function ($) {
    $body = (window.opera) ? (document.compatMode == "CSS1Compat" ? $("html") : $("body")) : $("html,body");
    $("#shang").mouseover(function () {
        up()
    }).mouseout(function () {
        clearTimeout(fq)

        ShowTestErrorMsg("调用 mouseout() 函数 wp-auto-top.js ");
    }).click(function () {
        $body.animate({
            scrollTop: 0
        },
        400)
    });
    $("#xia").mouseover(function () {
        dn()
    }).mouseout(function () {
        clearTimeout(fq);

        ShowTestErrorMsg("调用 mouseout() 函数 wp-auto-top.js ");
    }).click(function () {
        $body.animate({
            scrollTop: $(document).height()
        },
        400)
    });
    $("#comt").click(function () {
        $body.animate({
            scrollTop: $("#comments").offset().top
        },
        400)
    });
});
function up() {
    $wd = $(window);
    $wd.scrollTop($wd.scrollTop() - 1);
    fq = setTimeout("up()", 10);
    ShowTestErrorMsg("调用 up() 函数:" + $wd.scrollTop() );
};
function dn() {
    $wd = $(window);
    $wd.scrollTop($wd.scrollTop() + 1);
    fq = setTimeout("dn()", 10)
    ShowTestErrorMsg("调用 dn() 函数:" + $wd.scrollTop());
}; 