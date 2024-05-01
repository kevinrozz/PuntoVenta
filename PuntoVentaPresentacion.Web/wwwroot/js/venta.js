$(document).ready(function () {
    $("#codigoProducto").on("change", function () {
        var codigo = $(this).val();
        var detalleExistente = false;

        $(".codigoProducto").each(function () { 
            if ($(this).val() === codigo) {
                $("#codigoProducto").val("");
                var cantidad = parseInt($(this).closest(".row").find(".cantidadProducto").val());
                var precio = parseFloat($(this).closest(".row").find(".precioProducto").text());
                var newCantidad = cantidad + 1;
                var valorTotal = newCantidad * precio;
                $(this).closest(".row").find(".cantidadProducto").val(newCantidad);
                $(this).closest(".row").find(".valorTotalProducto").text(valorTotal.toFixed(2));
                detalleExistente = true;
                return false;
            }
        });

        if (!detalleExistente) {
            $.ajax({
                url: "/Productos/GetDetails?codigo=" + codigo,
                type: "GET",
                success: function (data) {
                    $("#codigoProducto").val("");
                    var prototipo = $(data);
                    var index = $("#detalleProducto .row").length;
                    $("#detalleProducto").append(prototipo);
                    prototipo.find(".clIdProducto").attr("name", "Detalles[" + index + "].IdProducto");
                    prototipo.find(".cantidadProducto").attr("name", "Detalles[" + index + "].Cantidad");
                    calcularTotalVenta();
                    calcularCambio();
                },
                error: function () {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "¡El producto no se encuentra!",
                    });
                }
            });
        } else {
            calcularTotalVenta();
            calcularCambio();
        }
    });

    $("#detalleProducto").on("change", ".cantidadProducto", function () {
        var cantidad = parseInt($(this).val());
        var precio = parseFloat($(this).closest(".row").find(".precioProducto").text());
        var valorTotal = cantidad * precio;
        $(this).closest(".row").find(".valorTotalProducto").text(valorTotal.toFixed(2));
        calcularTotalVenta();
        calcularCambio();
    });

    $("#MontoPago").on("change", function () {
        calcularCambio();
    });

    function calcularTotalVenta() {
        var totalVenta = 0;
        $(".valorTotalProducto").each(function () {
            totalVenta += parseFloat($(this).text());
        });
        $("#Total").val(totalVenta.toFixed(2));
    }

    function calcularCambio() {
        var totalVenta = parseFloat($("#Total").val());
        var montoPago = parseFloat($("#MontoPago").val());
        var cambio = montoPago - totalVenta;
        $("#tempCambio").val(cambio.toFixed(2));

        if (cambio < 0)
            $(".btn-primary").attr("disabled", "disabled");
        else
            $(".btn-primary").removeAttr("disabled");
    }

    $("#detalleProducto").on("click", ".btnEliminarProducto", function () {
        $(this).closest(".row").remove();
        calcularTotalVenta();
        calcularCambio();
    });
});

document.getElementById('formVenta').addEventListener('submit', function (event) {
    event.preventDefault();
    $.ajax({
        url: 'Create',
        type: 'POST',
        data: $('#formVenta').serialize(),
        success: function (response) {
            Swal.fire({
                icon: "success",
                text: "¡Venta registrada exitosamente!",
            }).then((result) => {
                window.location.href = 'Create'; 
            });
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }
    });
});