$(document).ready(function () {

    // Trumbowyg 

    $('#text-editor').trumbowyg({
        btns: [
            ['viewHTML'],
            ['undo', 'redo'], // Only supported in Blink browsers
            ['formatting'],
            ['strong', 'em', 'del'],
            ['superscript', 'subscript'],
            ['link'],
            ['insertImage'],
            ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull'],
            ['unorderedList', 'orderedList'],
            ['horizontalRule'],
            ['removeformat'],
            ['fullscreen'],
            ['foreColor', 'backColor'],
            ['emoji'],
            ['fontfamily'],
            ['fontsize'],
            ['preformatted']
        ],
        plugins: {
            colors: {
                foreColorList: [
                    'ff0000', '00ff00', '0000ff', '046582', 'FF6633', 'FFB399', 'FF33FF', 'FFFF99', '00B3E6', 'E6B333', '3366E6', '999966', '99FF99', '#B34D4D', '80B300', '809900', 'E6B3B3', '6680B3', '66991A', 'FF99E6', 'CCFF1A', 'FF1A66', 'E6331A', '33FFCC', '66994D', 'B366CC', '4D8000', 'B33300', 'CC80CC', '66664D', '991AFF', 'E666FF', '4DB3FF', '1AB399', 'E666B3', '33991A', 'CC9999', 'B3B31A', '00E680', '4D8066', '809980', 'E6FF80', '1AFF33', '999933', 'FF3380', 'CCCC00', '66E64D', '4D80CC', '9900B3', 'E64D66', '4DB380', 'FF4D4D', '99E6E6', '6666FF'
                ],
                backColorList: [
                    '000', '333', '555', "a2f8a5", "e23dd0", "d3486d", "00f7f9", "474893", "3cec35",
                    "1c65cb", "5d1d0c", "2d7d2a", "ff3420", "5cdd87", "a259a4", "e4ac44",
                    "1bede6", "8798a4", "d7790f", "b2c24f", "de73c2", "d70a9c", "25b67",
                    "88e9b8", "c2b0e2", "86e98f", "ae90e2", "1a806b", "436a9e", "0ec0ff",
                    "f812b3", "b17fc9", "8d6c2f", "d3277a", "2ca1ae", "9685eb", "8a96c6",
                    "dba2e6", "76fc1b", "608fa4", "20f6ba", "07d7f6", "dce77a", "77ecca"
                ],
                displayAsList: false,
                resizimg: {
                    minSize: 64,
                    step: 16,
                }

            }
        }
    });

    // Trumbowyg

    // Select2

    $('#categoryList').select2({
        theme: 'bootstrap4',
        placeholder: "Lütfen bir kategori seçiniz...",
        allowClear: true
    });

    // jQuery UI - DatePicker


    $("#datepicker").datepicker({
        closeText: "kapat",
        prevText: "&#x3C;geri",
        nextText: "ileri&#x3e",
        currentText: "bugün",
        monthNames: [
            "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
            "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"
        ],
        monthNamesShort: [
            "Oca", "Şub", "Mar", "Nis", "May", "Haz",
            "Tem", "Ağu", "Eyl", "Eki", "Kas", "Ara"
        ],
        dayNames: ["Pazar", "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi"],
        dayNamesShort: ["Pz", "Pt", "Sa", "Ça", "Pe", "Cu", "Ct"],
        dayNamesMin: ["Pz", "Pt", "Sa", "Ça", "Pe", "Cu", "Ct"],
        weekHeader: "Hf",
        dateFormat: "dd.mm.yy",
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: "",
        duration: 500,
        minDate: -3,
        maxDate:+3
    });
});



    // jQuery UI - DatePicker


