import { Component, OnInit } from "@angular/core";
import { AuthService } from "../_services/auth.service";
import { error } from "util";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.css"]
})
export class RegisterComponent implements OnInit {
  model: any = {};
  constructor(private authService: AuthService) {}

  ngOnInit() {}

  register() {
    this.authService.login(this.model).subscribe(
      () => {
        console.log("Registration successful");
      },
      error => {
        console.log(error);
      }
    );
  }
  cancel() {
    console.log("Canceled");
  }
}
