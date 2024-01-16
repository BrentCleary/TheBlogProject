let index = 0;

function AddTag() {
    // Get Reference to the TagEntry input element
    var tagEntry = document.getElementById("TagEntry");

    // Lets use the new search function to help detect an error state
    let searchResult = search(tagEntry.value);
    if (searchResult != null) {
        // Trigger sweet alert for whatever condition is contained in the search results var
        Swal.fire({
            title: 'Error!',
            text: 'Do you want to continue',
            icon: 'error',
            confirmationButtonText: 'Cool'
        })
    }
    else {
        // Create a new Select Option
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagList").options[index++] = newOption;
    }

    // Clear out the TagEntry control
    tagEntry.value = "";
    return true;
}

function DeleteTag() {

    let tagCount = 1;
    while (tagCount > 0) {
        let tagList = document.getElementById("TagList");
        let selectedIndex = tagList.selectedIndex;
        if (selectedIndex >= 0) {
            document.getElementById("TagList").options[selectedIndex] = null;
            --tagCount;
        }
        else {
            tagCount = 0;
            index--;
        }
    }
}

$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected");
})

// Look for the tagValues variable to see if it has data
if( tagValues != '') {
    let tagArray = tagValues.split(",");
    for (let loop = 0; loop < tagArray.length; loop++) {
        // Load up or Replace the options that we have
        ReplaceTag(tagArray[loop], loop);
        index++;
    }
}

function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag);
    document.getElementById("TagList").options[index] = newOption;
}


// The Search function will detect either an empty of a duplicate Tag
// and return and error string if an error is detected

function search(str) {
    if (str === "") {
        return "Empty Tags are not permitted";
    }

    var tagsEl = document.getElementById("TagList");

    if (tagsEl) {
        let options = tagsEl.options;
        for (let index = 0; index < options.length; i++) {
            if (options[index].value == str) {
                return `The Tag #${str} was detected as a duplicate and not permitted.`;
            }
        }
    }
}