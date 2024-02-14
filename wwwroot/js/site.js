VMasker(document.getElementById("cpfInput")).maskPattern("999.999.999-99");
VMasker(document.getElementById("cnpjInput")).maskPattern("99.999.999/9999-99");
VMasker(document.getElementById("cpf")).maskPattern("999.999.999-99");
VMasker(document.getElementById("cnpj")).maskPattern("99.999.999/9999-99");

$("input[name='loginType']").change(function () {

    var selectedValue = $(this).val();

    if (selectedValue === "cpf") {
        $("#cpfGroup").show();
        $("#cnpjGroup").hide();
        $("#cpfInput").val("");
    } else if (selectedValue === "cnpj") {
        $("#cpfGroup").hide();
        $("#cnpjGroup").show();
        $("#cnpjInput").val("");
    } else {
        $("#cpfGroup").hide();
        $("#cnpjGroup").hide();
        $("#cpfInput").val("");
        $("#cnpjInput").val("");
    }
});