$(document).ready(function () {
    // Handle change event of the parent select option
    $("#customForm").submit(function (event) {
        if ($("#childSelect").val() === null) {
            event.preventDefault(); // Prevent form submission
            alert("Please select fish species.");
        }
    });


    $('#parentSelect').change(function () {
        var fishingSpotId = $(this).val(); // Get the selected value of the parent select option

        // Send AJAX request to the server
        $.ajax({
            url: '/FishingSpots/GetFishingSpotSpeciesOptions',
            type: 'GET',
            data: { fishingSpotId: fishingSpotId }, // Pass the selected value as a parameter
            success: function (data) {
                // Clear the existing child select options
                $('#childSelect').empty();

                // Add new child select options based on the received data

                $('#childSelect').append("<option disabled selected>- Select fish species -</option>");

                $.each(data, function (index, option) {
                    $('#childSelect').append($('<option></option>').val(option.value).text(option.text));
                });
            },
            error: function () {
                // Handle error case
                alert('Error occurred while retrieving child options.');
            }
        });
    });
});
