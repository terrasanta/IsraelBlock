$(document).ready(function () {
    //alert("teste");
    $('#finalizaVenda').on("click", function () {
        $("#Closed").val("S");
        console.log($("#Closed").val());
    });
});