$( "#create-user" )
   .click(function() {
    $( "#dialog-modal" ).dialog( "open" );
});

$(function() {
    $( "#dialog-modal" ).dialog({
        autoOpen: false,
        resizable: false,
        modal: true,
        buttons: {
            "Ok": function() {
                $( this ).dialog( "close" );
            },
        }
    });
});