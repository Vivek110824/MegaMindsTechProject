$(document).ready(function () {

    loadUsers();

    $("#btnAdd").click(function () {
        $(".modal-title").text("Add User");
        $("#userForm")[0].reset();
        $("#Id").val(0);
        $("#btnSubmit").prop("disabled", true);
        $("#userModal").modal('show');
    });

    $("#Agree").change(function () {
        $("#btnSubmit").prop("disabled", !this.checked);
    });

    $("#Phone").on("input", function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    $("#State").change(function () {
        let state = $(this).val();
        $("#City").empty().append('<option value="">Select City</option>');

        if (state == "1") {
            $("#City").append('<option value="1">Surat</option>');
            $("#City").append('<option value="2">Bardoli</option>');
            $("#City").append('<option value="3">Baroda</option>');
        } else if (state == "2") {
            $("#City").append('<option value="4">Mumbai</option>');
            $("#City").append('<option value="5">Pune</option>');
        }
    });

    $("#userForm").submit(function (e) {
        e.preventDefault();

        let name = $("#Name").val().trim();
        let email = $("#Email").val().trim();
        let phone = $("#Phone").val().trim();
        let state = $("#State").val();
        let city = $("#City").val();

        let emailPattern = new RegExp("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$");

        if (name === "") return alert("Name is required");
        if (!emailPattern.test(email)) return alert("Invalid email");
        if (phone.length !== 10) return alert("Phone must be 10 digits");
        if (!state) return alert("Select State");
        if (!city) return alert("Select City");

        let data = {
            Id: $("#Id").val(),
            Name: name,
            Email: email,
            Phone: phone,
            Address: $("#Address").val(),
            StateId: state,
            CityId: city
        };

        $.post('/User/Save', data, function () {
            $("#userModal").modal('hide');
            loadUsers();
        });
    });

});

function loadUsers() {
    $.get('/User/GetUsers', function (data) {
        let html = "";

        $.each(data, function (i, item) {
            html += `<tr>
                        <td>${item.name}</td>
                        <td>${item.email}</td>
                        <td>${item.phone}</td>
                        <td>
                            <button class="btn btn-warning btn-sm" onclick="editUser(${item.id})">Edit</button>
                            <button class="btn btn-danger btn-sm" onclick="deleteUser(${item.id})">Delete</button>
                        </td>
                    </tr>`;
        });

        $("#userTable").html(html);
    });
}

function editUser(id) {
    $(".modal-title").text("Edit User");

    $.get('/User/GetById?id=' + id, function (data) {

        $("#Id").val(data.id);
        $("#Name").val(data.name);
        $("#Email").val(data.email);
        $("#Phone").val(data.phone);
        $("#Address").val(data.address);

        $("#State").val(data.stateId).change();

        setTimeout(function () {
            $("#City").val(data.cityId);
        }, 200);

        $("#btnSubmit").prop("disabled", false);
        $("#Agree").prop("checked", true);

        $("#userModal").modal('show');
    });
}

function deleteUser(id) {
    if (confirm("Are you sure?")) {
        $.post('/User/Delete', { id: id }, function () {
            loadUsers();
        });
    }
}