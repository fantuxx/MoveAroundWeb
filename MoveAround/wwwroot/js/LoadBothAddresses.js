let autocomplete2;
let placeSearch2;
let placeSearch;
let autocomplete;
const componentForm = {
    street_number: "short_name",
    route: "long_name",
    locality: "long_name",
    administrative_area_level_1: "short_name",
    country: "long_name",
    postal_code: "short_name",
};
const componentForm2 = {
    street_number: "short_name",
    route: "long_name",
    locality: "long_name",
    administrative_area_level_1: "short_name",
    country: "long_name",
    postal_code: "short_name",
    

};

function initAutocomplete() {
    autocomplete2 = new google.maps.places.Autocomplete(
        document.getElementById("AutoIAdresas"),
        { types: ["geocode"] }
    );
    autocomplete = new google.maps.places.Autocomplete(
        document.getElementById("AutoIsAdresas"),
        { types: ["geocode"] }
    );
    // Avoid paying for data that you don't need by restricting the set of
    // place fields that are returned to just the address components.
    // autocomplete.setFields(["address_component"]);
    // When the user selects an address from the drop-down, populate the
    // address fields in the form.
    autocomplete.addListener("place_changed", fillInAddress);
    autocomplete2.addListener("place_changed", fillInAddress2);
}

function fillInAddress() {
    // Get the place details from the autocomplete object.
    var place = autocomplete.getPlace();
    document.getElementById('lat').value = place.geometry.location.lat();
    document.getElementById('lon').value = place.geometry.location.lng();


    for (const component in componentForm) {
        document.getElementById(component).value = "";
        document.getElementById(component).disabled = false;
    }

    // Get each component of the address from the place details,
    // and then fill-in the corresponding field on the form.
    for (const component of place.address_components) {
        const addressType = component.types[0];

        if (componentForm[addressType]) {
            const val = component[componentForm[addressType]];
            document.getElementById(addressType).value = val;
        }
    }

}

// Bias the autocomplete object to the user's geographical location,
// as supplied by the browser's 'navigator.geolocation' object.
function geolocate() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition((position) => {
            const geolocation = {
                lat: position.coords.latitude,
                lng: position.coords.longitude,
            };
            const circle = new google.maps.Circle({
                center: geolocation,
                radius: position.coords.accuracy,
            });
            autocomplete.setBounds(circle.getBounds());
        });
    }
}
function fillInAddress2() {

    var place2 = autocomplete2.getPlace();//tas veikia, gauname vieta  
    document.getElementById('lat2').value = place2.geometry.location.lat();
    document.getElementById('lon2').value = place2.geometry.location.lng();

    for (comp in componentForm2) {
        var zipcode = comp + "2";
        document.getElementById(zipcode).value = "";
    }

    for (const component of place2.address_components) {
        const addressType = component.types[0];
        

        if (componentForm2[addressType]) {
            const val = component[componentForm2[addressType]];

            document.getElementById(addressType+"2").value = val;
        }
    }

}


