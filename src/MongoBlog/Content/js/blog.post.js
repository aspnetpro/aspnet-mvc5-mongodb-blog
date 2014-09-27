/// <reference path="../libs/jquery/jquery-2.0.3.min.js" />
/// <reference path="../libs/validate/jquery.validate.js" />

jQuery(function ($) {

    $('#comments-list').on('click', '[data-action=load-comments]', function (e) {
        e.preventDefault(); e.stopPropagation();

        var $link = $(this);
        $link.button('loading');

        //atrasa a chamada dos posts em 2 segundos
        //setTimeout(function () {

            //chama os próximos posts via ajax
            $.get($link.attr('href'))
                .done(function (result) {
                    $(result).hide().appendTo("#comments-list").fadeIn();
                    $link.remove();
                })
                .fail(function () {
                    alert("Cannot load comments!");
                });

        //}, 2000);
        
    });

    //validate
    $('#form-add-comment').validate({
        rules: {
            "author": "required",
            "email": {
                "required": true,
                "email": true
            },
            body: "required"
        },
        messages: {
            "author": "Autor é obrigatório",
            "email": {
                "required": "Email é obrigatório",
                "email": "Email inválido"
            },
            body: "Mensagem é obrigatório"
        }
    });

    //actions form
    $('[data-action=add-comment]').on('click', function () {
        $('#form-add-comment').submit();
    });

});