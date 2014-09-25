/// <reference path="../libs/jquery/jquery-2.0.3.min.js" />
/// <reference path="../libs/validate/jquery.validate.js" />

jQuery(function ($) {

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