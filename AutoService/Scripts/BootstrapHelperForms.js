$(document).ready(function SetFormGroupClasses() {
    $(".form-group label").addClass("col-lg-3 col-md-3 col-sm-2 col-xs-4 col-xs-offset-2 col-sm-offset-0 col-md-offset-0 col-lg-offset-0");
    $(".form-group").each(function () {
        $(this).children("input, span, textarea").wrapAll(" <div class=\"col-lg-9 col-md-9 col-sm-10 col-xs-10 col-xs-offset-1 col-xs-offset-2 col-sm-offset-0 col-md-offset-0 col-lg-offset-0\"></div>");
    });
});