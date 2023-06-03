import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
//import { User } from 'src/app/models/user.model';
import { AuthenticationService } from 'src/app/services/auth.service';
import { RandomService } from 'src/app/services/random.service';
import { UserRecipeService } from 'src/app/services/userRecipe.service';
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  UserName: string | undefined;
  isAuth: boolean | undefined;
  codeUpdating: boolean = false;

  constructor(private authenticationService: AuthenticationService, private route: ActivatedRoute, private router: Router, private randomServ: RandomService) { }

  device: string | undefined | null;

  ngOnInit(): void {
    this.device = localStorage.getItem('device')
    if (this.device == null) {
      this.device = uuidv4();
      localStorage.setItem('device', this.device);
    }
    this.codeToTokens()

    this.UserName = localStorage.getItem('name') ?? '';
    this.isAuth = this.UserName == '' ? false : true;
  }

  loginRedir() {
    this.authenticationService.RedirectLogin()
  }

  logout() {
    this.authenticationService.logout()
    this.codeUpdating = false;
    this.isAuth = false;
    window.location.reload();
  }

  random() {

  }


  codeToTokens() {
    this.router.events
      .subscribe(e => {
        if (e.constructor.name === 'NavigationEnd' && this.router.navigated) {
          this.route.queryParams
            .subscribe(async params => {
              if (params['code'] != null) {
                this.codeUpdating = true;
                await this.authenticationService.GetTokens(params['code'])
                this.UserName = localStorage.getItem('name') ?? '';
                this.isAuth = this.UserName == '' ? false : true;
                window.location.replace("http://localhost:4200");
              }
            })
        }
      });
  }
}
