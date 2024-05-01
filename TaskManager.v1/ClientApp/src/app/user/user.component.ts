import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../Services/login.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  loginForm: any
  errorText: string = ""
  constructor(private loginService: LoginService, private router: Router) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      mailID: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required)
    })
  }

  login() {
    if (!this.loginForm.valid) {
      this.errorText = "Please enter email and password"
    }
    else {
      this.errorText = '';
      let request = {
        Mail: this.loginForm.value['mailID'],
        Password: this.loginForm.value['password']
      }
      this.loginService.login(request).subscribe((data) => {
        if (data.code == '500') {
          this.errorText = 'invalid username/Password';
        }
        else {
          this.errorText=''
          localStorage.removeItem('sessionID');
          localStorage.setItem('sessionID', data.token)
          this.router.navigate(['']);
        }        
      })
    }   
    
  }

  getToken() {
    console.log(localStorage.getItem('sessionID'))
  }

}
