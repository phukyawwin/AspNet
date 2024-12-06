$("#specialty").change(function () {
    const specialtyId = $(this).val();
    if (specialtyId) {
        loadDoctors(specialtyId); // Load doctors for the selected specialty
    } else {
        clearDropdown("#doctor"); // Clear doctor dropdown if no specialty selected
    }
    clearDropdown("#timeSlot"); // Clear time slots when specialty changes
});

// Function to load doctors for the selected specialty
function loadDoctors(specialtyId) {
    $.ajax({
        url: `/Doctor/GetDoctorsBySpecialty?specialtyId=${specialtyId}`,
        type: "GET",
        success: function (data) {
            populateDropdown("#doctor", data, "--Select Doctor--");
        },
        error: function () {
            alert("Failed to load doctors. Please try again.");
        }
    });
}

// Utility function to populate dropdowns
function populateDropdown(selector, data, defaultOption) {
    const dropdown = $(selector);
    dropdown.empty();
    dropdown.append(new Option(defaultOption, ""));
    data.forEach(item => {
        dropdown.append(new Option(item.Name, item.Id)); // Assuming doctor has 'Name' and 'Id'
    });
}

$(document).ready(function () {
    // Event handler when the doctor is selected
    $("#doctor").change(function () {
        const doctorId = $(this).val();
        if (doctorId) {
            loadAvailableDays(doctorId); // Load available days for the selected doctor
        }
    });

    // Event handler when the date is selected
    $("#date").change(function () {
        const doctorId = $("#doctor").val();
        const selectedDate = $(this).val();
        if (doctorId && selectedDate) {
            loadTimeSlots(doctorId, selectedDate); // Load time slots for the selected date
        }
    });
    flatpickr("#date", {
        dateFormat: "Y-m-d", // Format the date as YYYY-MM-DD
        minDate: "today", // Optional: disable past dates
    });
});

// Function to load available days for the selected doctor
function loadAvailableDays(doctorId) {
    $.ajax({
        url: `/Slot/GetAvailableDays?doctorId=${doctorId}`,
        type: "GET",
        success: function (data) {
            initDatePicker(data);// Enable only the available days
        },
        error: function () {
            alert("Failed to load available days. Please try again.");
        }
    });
}
function initDatePicker(availableDays) {
    // Initialize Flatpickr on the #date input field
    flatpickr("#date", {
        dateFormat: "Y-m-d", // Format the date as YYYY-MM-DD
        minDate: "today", // Disable past dates
        disable: [
            function (date) {
                // Disable days that are not in the availableDays array
                return !availableDays.includes(getDayName(date.getDay()));
            }
        ]
    });
}

// Helper function to convert numeric day to string (0 -> Sunday, 1 -> Monday, ...)
function getDayName(dayNumber) {
    const days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
    return days[dayNumber];
}
// Function to enable only available days in the datepicker
function enableAvailableDays(availableDays) {
    // Get the datepicker input element
    var $datepicker = $("#date");

    // Destroy the existing datepicker and initialize a new one
    $datepicker.datepicker("destroy").datepicker({
        dateFormat: "yy-mm-dd",
        beforeShowDay: function (date) {
            const dayOfWeek = date.getDay(); // Get the day of the week (0 = Sunday, 1 = Monday, etc.)
            // Enable only the available days (matching with the server response)
            return [availableDays.includes(getDayName(dayOfWeek))];
        }
    });
}

// Function to map day numbers to day names
function getDayName(dayIndex) {
    const dayNames = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
    return dayNames[dayIndex];
}

// Function to load time slots for the selected doctor and date
function loadTimeSlots(doctorId, selectedDate) {
    $.ajax({
        url: `/Slot/GetTimeSlots?doctorId=${doctorId}&date=${selectedDate}`,
        type: "GET",
        success: function (data) {
            populateDropdown("#timeSlot", data, "--Select Slot--");
        },
        error: function () {
            alert("Failed to load time slots. Please try again.");
        }
    });
}

// Utility function to populate dropdowns
function populateDropdown(selector, data, defaultOption) {
    const dropdown = $(selector);
    dropdown.empty();
    dropdown.append(new Option(defaultOption, ""));
    data.forEach(item => {
        dropdown.append(new Option(item.text, item.value));
    });
}

// Utility function to clear dropdowns
function clearDropdown(selector) {
    const dropdown = $(selector);
    dropdown.empty();
    dropdown.append(new Option("--Select--", ""));
}
