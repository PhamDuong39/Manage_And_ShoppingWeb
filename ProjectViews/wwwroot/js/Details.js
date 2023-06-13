$(document).ready(function () {
  $(".color-option").click(function () {
    $(".color-option").removeClass("selected-color");
    $(this).addClass("selected-color");
  });
  $(".sizeinput").click(function () {
    $(".sizeinput").removeClass("selected-size");
    $(this).addClass("selected-size");
  });
});
