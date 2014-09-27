/// <reference path="../libs/jquery/jquery-2.0.3.min.js" />
/// <reference path="../libs/validate/jquery.validate.js" />

jQuery(function ($) {

    //validate
    $('#form-contact').validate({
        rules: {
            "name": "required",
            "email": {
                "required": true,
                "email": true
            },
            "message": "required"
        },
        messages: {
            "name": "Nome é obrigatório",
            "email": {
                "required": "Email é obrigatório",
                "email": "Email inválido"
            },
            "message": "Mensagem é obrigatório"
        }
    });

});