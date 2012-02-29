// JavaScript Document
$(document).ready(function(){
	$(".close-button").click(function() {
		$(this).parent().toggle('slow', function(){
	});
});
	
	
$(".config-button").click(function () {
	$("#config-box").toggle("slide", { direction: "right" }, 1000);
});

$(".delete").click(function () {
	confirm ("Are you sure you want to delete this record?")
});

$(".link-button").click(function () {
	$(this).addClass("active");
});


$("#call-category").click(function () {
	$('#add-category').dialog({ resizable: false, minWidth: 500 });
});

$("#call-meeting").click(function () {
	$('#add-meeting').dialog({ resizable: false, minWidth: 567 });
});
	
	$("#formregister").submit(function() {
	
			if($("#email").val() != $("#email2").val()){
				alert("Emails do not match!");	
				location.reload();
				return false;
			}else if($("#pass").val() != $("#pass2").val()){
			    alert("Passwords do not match!");	
				location.reload();
				return false;
			}else{
				return true;	
			}
	});
});