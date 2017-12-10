$(document).ready(function () {
    $('.datepicker').datepicker({
        closeText: 'Закрыть',
        prevText: '&#x3c;Пред',
        nextText: 'След&#x3e;',
        currentText: 'Сегодня',
        monthNames: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь',
        'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
        monthNamesShort: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн',
        'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
        dayNames: ['воскресенье', 'понедельник', 'вторник', 'среда', 'четверг', 'пятница', 'суббота'],
        dayNamesShort: ['вск', 'пнд', 'втр', 'срд', 'чтв', 'птн', 'сбт'],
        dayNamesMin: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
        weekHeader: 'Нед',
        dateFormat: 'dd.mm.yy',
        firstDay: 1,
        showMonthAfterYear: false,
        yearSuffix: ''
    });

    $('.timepicker').timepicker({
        'step': 15,
        'minTime': '8:00am',
        'maxTime': '17:00pm',
        'timeFormat': 'H:i'
    });

    $(".yearpicker").datepicker({  
        stepMonths: 12,  
        yearRange: "c-50:c",  
        changeYear: true,
        changeMonth: false,
        dateFormat: 'dd.mm.yy',
        onClose: function(dateText, inst) { 
            var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
            $(this).datepicker('setDate', new Date(year, 0, 1));
        }
    });

    $(".yearpicker").focus(function () {
        $(".ui-datepicker-calendar").hide();
        $(".ui-datepicker-month").hide();
        $("#ui-datepicker-div").position({
            my: "center bottom",
            at: "center top",
            of: $(this)
        });    
    });
});

function configureTimePicker(step, startTime, endTime) {
    $('.timepicker').timepicker({
        'step': step,
        'minTime': startTime,
        'maxTime': endTime,
        'timeFormat': 'H:i'
    });
}