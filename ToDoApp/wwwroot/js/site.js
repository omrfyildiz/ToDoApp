$(document).ready(function () {

    //addItem function will work when be clicked Add button
    $('#add-item-button').on('click', addItem);

    //done-checkbox classes will work when be clicked checkbox
    $('.done-checkbox').on('click', function (e) {
        markCompleted(e.target);
    });
});

function addItem() {
    $('#add-item-error').hide();
    var newTitle = $('#add-item-title').val();

    $.post('/ToDo/AddItem', { title: newTitle }, function () {
        window.location = '/ToDo';
    })

        .fail(function (data) {
            if (data && data.responseJSON) {
                var firstError = data.responseJSON[Object.keys(data.responseJSON)[0]];
                $('#add-item-error').text(firstError);
                $('#add-item-error').show();
            }
        });
}

function markCompleted(checkbox) {
    checkbox.disabled = true;

    $.post('/ToDo/MarkDone', { id: checkbox.name }, function () {
        var row = checkbox.parentElement.parentElement;
        $(row).addClass('done');
    });

}



