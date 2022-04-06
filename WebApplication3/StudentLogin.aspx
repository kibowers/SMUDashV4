<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentLogin.aspx.cs" Inherits="WebApplication3.WebForm1" %>



<script>
	//using this top script as a comment box. Things this page needs implemented:

	/*
	 * Actual login functionality 
	 * Javascript fade in animation on login boxes
	 * A top bar with white surrounding the "SMU Portal Access" portion. 
	 * Input validation of some kind
	 * Working redirections to the professor login page 
	 * 
	 * 
	 * 
	 * For now, we will focus on login functionality and gettting the professor login page working, but we will come back to the other portions at a future date.
	 * /
	 * 
	 * //
	 * */
	
</script>
<script src="C:\Users\Kaleb\source\repos\WebApplication3\WebApplication3\Scripts\jquery-3.4.1.min.js"></script>
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
<script src="C:\Users\Kaleb\source\repos\WebApplication3\WebApplication3\Scripts\bootstrap.min.js"></script>

<!DOCTYPE html>



<style>
	h3, h1, h2, input, h4, label, p, button, small {
		font-family: 'Century Gothic';
	}
</style>


<style>
	.login-form-bottom-gradient {
		background: linear-gradient(to bottom right, white, blue, gold);
	}

	body {
		background: url(https://upload.wikimedia.org/wikipedia/commons/a/a3/-_Singapore_Management_University_School_of_Law_Armeninan_Street.jpg);
	}
</style>




<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<script src="C:\Users\Kaleb\source\repos\WebApplication3\WebApplication3\Scripts\jquery-3.4.1.min.js"></script>
<script src="C:\Users\Kaleb\source\repos\WebApplication3\WebApplication3\Scripts\bootstrap.min.js"></script>

    <title> SMU Login</title>
</head>







	<body> 
		<div class="text-center">
    <h1 class="display-4">SMU Portal Access</h1>
    <p>Students can login using their credentials below. Professors may login <a href= "/ProfessorHub.cshtml/"> here.</a></p>
</div>



<section class="h-100 gradient-form ">
	<div class="container py-5 h-100">
		<div class= "row d-flex justify-content-center align-items-center h-100">
			<div class= "col-xl-10">
				<div class= "card rounded-3 text-black">
					<div class= "row g-0">
						<div class = "col-lg-6">
							<div class="card-body p-md-5 mx-md-4">
								<form runat="server">
	<div class="form-outline mb-4">
		
		<div class="text-center">
					<img src="https://cdn-assets-eu.frontify.com/s3/frontify-enterprise-files-eu/eyJwYXRoIjoic2luZ2Fwb3JlLW1hbmFnZW1lbnQtdW5pdmVyc2l0eVwvYWNjb3VudHNcLzc0XC80MDAwOTk4XC9wcm9qZWN0c1wvMzRcL2Fzc2V0c1wvNmNcLzIzMjVcLzg0Njk2ZDE4ZjVmNGU4MGNkNDM5NTZkYjU5MGRiNTE2LTE2MjMyOTMzNTYucG5nIn0:singapore-management-university:JHR2-p2HFfo5ewE_kX_ZUsB-oZdCAiM8ZIDLzJo7pvo?width=354" width =150px;/>
					<h3>
						SMU Student Login
					</h3>
</div>

		<label for="email1"> Email Address </label>
		<input type="email" class="form-control" id="email1" aria-describedby="emailMsg" placeholder="Students, please enter your email."  runat="server"/>
		<small id="emailMsg" class="form-text text-muted"> Student emails should be entered in this input. </small>
		<br />
		
	</div>
	<div class="form-outline mb-4">

		<label for="pass1"> Password </label>
		<input type="password" class="form-control" id="pass1" aria-describedby="passMsg" placeholder="Enter your password here." runat="server" />
		<small id="passMsg" class="form-text text-muted"> Passwords should be entered here. </small>
	</div>

	<div class="text-center pt-1 mb-5 pb-1">
		<button class="btn btn-primary btn-block fa-lg gradient-cutom-2 mb-3" type="button" runat="server"> Login </button>
	</div>
	</form>



							</div>

						</div>

						<div class="col-lg-6 d-flex align-items-center login-form-bottom-gradient">
							<div class="text-white px-3 py-4 p-md-5 mx-md-4">
								<h3 class="mb-4"> 
									Professors....
								</h3>
								<img src="https://thecollegepost.com/wp-content/uploads/2018/08/How-many-courses-do-university-faculty-teach1.jpg" width = 300px;/>
								<br />
								<p class="small mb-0">
									Professors can access their login page below.  
								</p>
								<div class="text-center pt-1 mb-5 pb-1">
								<button class="btn btn-outline-light" type="button" runat="server"> Professor Login </button>
								</div>

							</div>
						</div>


					</div>
				</div>
			</div>
		</div>

	</div>




	
</section>

		</body>
	</html>