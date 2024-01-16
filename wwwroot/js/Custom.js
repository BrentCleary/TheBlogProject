let index = 0;

function AddTag() {
    // Get Reference to the TagEntry Input
    var tagEntry = document.getElementById("TagEntry");

    // Create a new Select Option
    let newOption = new Option(tagEntry.value, tagEntry.value);

    document.getElementById("TagList").options[index++] = newOption;

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