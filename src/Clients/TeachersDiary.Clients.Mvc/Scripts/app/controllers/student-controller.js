$(function ($) {
    "use strict";
    $(document).ready(function () {
        //grid settings

        var table = $('#student-grid').DataTable({
            "bSort": false,
            paging: false,
            "bFilter": false,
            "bInfo": false,
            "scrollX": true,
            pagingType: "full_numbers",
            dom: '<"html5buttons"B>lTfgitp',
            buttons: [{
                extend: 'print',
                text: 'Принтирай',
                message: '<h2>' + $('#class-name').text() + '</h2>',
                exportOptions: {
                    stripHtml: false
                },
                customize: function (win) {
                    var $body = $(win.document.body);

                    $body.addClass('white-bg');
                    $body.css('font-size', '10px');

                    $body.find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');


                    $body.find('thead').prepend(
                        " <th class='text-center' colspan='2'>Ученик</th>" +
                        " <th class='text-center' colspan='2'>Септември</th> " +
                        " <th class='text-center' colspan='2'>Октомври</th> " +
                        " <th class='text-center' colspan='2'>Ноември</th> " +
                        " <th class='text-center' colspan='2'>Декември</th> " +
                        " <th class='text-center' colspan='2'>Януари</th> " +
                        "<th class='text-center' colspan='2'>Февруари</th> " +
                        "<th class='text-center' colspan='2'>Март</th> " +
                        "<th class='text-center' colspan='2'>Април</th>" +
                        " <th class='text-center' colspan='2'>Май</th>" +
                        " <th class='text-center' colspan='2'>Юни</th>" +
                        " <th class='text-center' colspan='2'>Общо</th>");


                    $body.find('.total-absences').each(function (i) {
                        var val = $(this).val();
                        $(this).next().text(val);
                        $(this).css('display', 'none');
                    });

                    $body.find('.hidden-values')
                        .css('display', 'block');
                }
            }, {
                text: 'Изчисли',
                className: 'calculate-btn'
            }]
        });

        $('#student-grid tbody').on('click', 'tr', function () {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            } else {
                table.$('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }
        });

        $('#button').click(function () {
            table.row('.selected').remove().draw(false);
        });

        $('.calculate-btn').on('click', function () {
            $('#myModal').modal();
         });

        $('#sent-data-btn').on('click', function () {
            $('#student-form').submit();
         });
    });
}($));