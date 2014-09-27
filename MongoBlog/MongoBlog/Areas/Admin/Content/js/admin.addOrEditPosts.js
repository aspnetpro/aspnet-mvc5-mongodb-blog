jQuery(function ($) {

    $postBody = $('#post-body');
    $inputCategory = $('#inputCategory');
    $inputTags = $('#inputTags');

    $postBody.redactor({
        minHeight: 355,
        placeholder: 'Add content here...',
        imageUpload: $postBody.attr('data-upload'),
        imageGetJson: $postBody.attr('data-gallery')
    });

    $('#post-form').validate({
        rules: {
            title: 'required',
            summary: 'required',
            category: 'required'
        }
    });

    $inputTags.tagsInput({
        autocomplete_url: $inputTags.attr('data-ajax-source'),
        height: 'auto',
        width: 'auto'
    });

    $inputCategory.autocomplete({
        source: $inputCategory.attr('data-ajax-source')
    });

});