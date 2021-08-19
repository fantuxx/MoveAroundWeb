$(document).ready(function () {
    $('#AppUsers').dataTable({       
        "ajax": {
            "url": "/AddToRoles/GetAll",
            "type": "GET",
            "datatype": "json"
        },        
        "columns": [
            { "data": "firstName",  "autoWidth": true },
            { "data": "lastName", "autoWidth": true },
            { "data": "buisnessName",  "autoWidth": true },
            { "data": "buisnesAdressCity",  "autoWidth": true },      
            { "data": "user.userName", "autoWidth": true },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class = "text-center">
                        <a href = "/AddToRoles/edit?id=${data}" class='btn ' style='cursor:pointer; width:70;'>
                            <i class="fas fas fa-edit fa-1x text-sviesus-3 "></i>
                        </a>
                        &nbsp;
                        
                            &nbsp;
                        <a href = "/AddToRoles/AddMore?id=${data}" class='btn ' style='cursor:pointer; width:70;'>
                            <i class="far fa-plus-square fa-1x text-sviesus-3 "></i>
                        </a>
                       
                       </div>`;
                }, "width": "40%"
            }
            
            //SUKURTI CONTROLERI IR VIEW SU ATRASYMU
        ]
    });
});  
