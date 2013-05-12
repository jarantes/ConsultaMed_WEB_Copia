function getEndereco() {
    var aux = $("#cep").val();
    if ($.trim(aux) != "_____-___" ) {
        $("#loading").fadeIn();
        $.getScript("http://cep.republicavirtual.com.br/web_cep.php?formato=javascript&cep=" + $("#cep").val(), function () {
            if (window.resultadoCEP["resultado"] != 0) {
                $("#rua").val(unescape(window.resultadoCEP["tipo_logradouro"]) + " " + unescape(window.resultadoCEP["logradouro"]));
                $("#bairro").val(unescape(window.resultadoCEP["bairro"]));
                $("#cidade").val(unescape(window.resultadoCEP["cidade"]));
                $("#estado").val(unescape(window.resultadoCEP["uf"]));
                $("#loading").hide();
            } else {
                $("#loading").hide();
                $('#dialog').modal({
                    keyboard: false
                });
            }
        });
    }
}