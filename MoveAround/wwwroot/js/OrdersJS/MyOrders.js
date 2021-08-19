$(document).ready(function () {
    $('#Orders').dataTable({
        "sDom": "Rlfrtip",// tik sis del resize
        "dom": 'BRrtlsip',
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "Visi"]],
        responsive: true,
        select: true,
        buttons: [
            {
                text: 'Visi užsakymai',
                className: "btn ml-4 btn-danger my-1 btn-sm",
                action: function (dt) {
                    window.location.href = "/Orders/All/";
                }
            },
            {
                extend: 'colvis',
                text: 'Stulpeliai',
                className: "btn btn-danger my-1 btn-sm",

            },
            {
                extend: 'collection',
                className: "btn btn-danger my-1 btn-sm",
                text: 'Eksportuoti',
                buttons: [
                    {
                        extend: 'pdf',
                        className: "btn btn-danger mx-1 my-2 btn-sm",
                        text: 'Gauti PDF',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        className: "btn btn-danger mx-1 my-1 btn-sm",
                        exportOptions: {
                            columns: ':visible'
                        },
                    },

                    {
                        extend: 'excel',
                        className: "btn btn-danger mx-1 my-1 btn-sm",
                        text: 'Excel',
                        exportOptions: {
                            columns: ':visible'
                        },
                    },
                    {
                        extend: 'print',
                        className: "btn btn-danger mx-1 my-1 btn-sm",
                        text: 'Spausdinti',
                        pageSize: 'LEGAL',
                        orientation: 'landscape',
                        exportOptions: {
                            columns: ':visible'
                        },
                    },
                    {
                        extend: 'selectAll',
                        className: "btn btn-danger mx-1 my-1 btn-sm",
                        text: 'Pasirinkti viską',
                    },


                ]
            },
            {
                extend: 'collection',
                className: "btn btn-danger my-1 btn-sm",
                text: 'Naujas',
                buttons: [
                    {
                        text: 'Naujas',
                        className: "btn btn-danger my-1 btn-sm",
                        action: function (dt) {
                            window.location.href = "/Orders/Create/";
                        }
                    },
                    {
                        extend: 'selectedSingle',
                        text: 'Naujas pagal pažymėtą',
                        className: "btn btn-danger my-1 btn-sm",
                        action: function (e, dt, node, config) {
                            var data = $('#Orders').DataTable().row('.selected').data();
                            window.location.href = "/Orders/NewAlike/" + data.orderId;
                        }
                    },

                ]
            },
            {
                extend: 'selectedSingle',
                className: "btn btn-danger my-1 btn-sm",
                text: 'Redaguoti',
                action: function (e, dt, node, config) {
                    var data = $('#Orders').DataTable().row('.selected').data();
                    window.location.href = "/Orders/Edit/" + data.orderId;
                }
            },
            {
                extend: 'selectedSingle',
                text: 'Trinti',
                className: "btn btn-danger my-1 btn-sm",
                action: function (e, dt, node, config) {
                    var data = $('#Orders').DataTable().row('.selected').data();
                    window.location.href = "/Orders/Delete/" + data.orderId;


                }
            },
            {
                extend: 'selectedSingle',
                text: 'Išsamiau',
                className: "btn btn-danger my-1 btn-sm",
                action: function (e, dt, node, config) {
                    var data = $('#Orders').DataTable().row('.selected').data();
                    window.location.href = "/Orders/Details/" + data.orderId;
                }
            },



        ],

        "language": {
            select: {
                rows: {
                    _: ". Jūs pasirinkote %d eilučių",
                    0: ". Spauskite ant eilutės norėdami ją pasirinkti",
                    1: ". Pasirinkta tik vieną eilutę"
                }
            },
            "sEmptyTable": "Lentelėje nėra duomenų",
            "sInfo": "Rodomi įrašai nuo _START_ iki _END_ iš _TOTAL_ įrašų",
            "sInfoEmpty": "Rodomi įrašai nuo 0 iki 0 iš 0",
            "sInfoFiltered": "(atrinkta iš _MAX_ įrašų)",
            "sInfoPostFix": "",
            "sInfoThousands": " ",
            "sLengthMenu": "Rodyti _MENU_ įrašus",
            "sLoadingRecords": "Įkeliama...",
            "sProcessing": "Apdorojama...",
            "sSearch": "Ieškoti:",
            "sThousands": " ",
            "sUrl": "",
            "sZeroRecords": "Įrašų nerasta",

            "oPaginate": {
                "sFirst": "Pirmas",
                "sPrevious": "Ankstesnis",
                "sNext": "Tolimesnis",
                "sLast": "Paskutinis",
                "row selectec": "eilučių pasirinkta"
            }
        },
        "ajax": {
            "url": "/Orders/MyOrders",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "fromAddress", "autoWidth": false },
            {
                "data": "fDate",
                "type": "date ",
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getFullYear() + "-" + (month.toString().length > 1 ? month : "0" + month) + "-" + date.getDate();
                }
            },
            { "data": "toAddress", "autoWidth": false },
            {
                "data": "tDate", "type": "date ",
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getFullYear() + "-" + (month.toString().length > 1 ? month : "0" + month) + "-" + date.getDate();
                }
            },
            { "data": "tipas", "autoWidth": true },
            { "data": "pakrovimoTipas", "autoWidth": true },
            { "data": "svoris", "autoWidth": true, "visible": true },
            { "data": "paleciuSk", "autoWidth": true, "visible": true },
            { "data": "turis", "autoWidth": true, "visible": true },
            { "data": "ilgis", "autoWidth": true, "visible": true },
            { "data": "temperatura", "autoWidth": true, "visible": false },
            { "data": "kaina", "autoWidth": true, "visible": true },
            { "data": "papildomaInfo", "autoWidth": true, "visible": false },
            { "data": "orderId", "autoWidth": true, "visible": false },


        ],




    });


});  
