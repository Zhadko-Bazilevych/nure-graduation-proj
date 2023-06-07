import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
//import { User } from 'src/app/models/user.model';
import { AuthenticationService } from 'src/app/services/auth.service';
import { RandomService } from 'src/app/services/random.service';
import { RecipeService } from 'src/app/services/recipe.service';
import { UserRecipeService } from 'src/app/services/userRecipe.service';
import { GlobalDataService } from 'src/app/services/globalData.service';
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private authenticationService: AuthenticationService, private route: ActivatedRoute, private router: Router, private randomServ: RandomService, public global: GlobalDataService, private recipeService: RecipeService) { }

  device: string | undefined | null;

  ngOnInit(): void {
    this.device = localStorage.getItem('device')
    if (this.device == null) {
      this.device = uuidv4();
      localStorage.setItem('device', this.device);
    }
    this.codeToTokens()
  }

  loginRedir() {
    this.authenticationService.RedirectLogin()
  }

  logout() {
    this.authenticationService.logout()
  }

  random() {
    // this.recipeService.random().then(
    //   response => {
    //     if(response.code == 200){
    //       this.router.navigate(['/recipe/' + response.id]);
    //     }
    //   }
    // )
  }


  codeToTokens() {
    this.router.events
      .subscribe(e => {
        if (e.constructor.name === 'NavigationEnd' && this.router.navigated) {
          this.route.queryParams
            .subscribe(async params => {
              if (params['code'] != null) {
                this.global.isLoading = true;
                await this.authenticationService.GetTokens(params['code'])
                await this.global.getUserData();
                this.router.navigate([], {
                  queryParams: { 'code': null, 'scope' : null, 'authuser' : null, 'prompt' : null },
                  queryParamsHandling: 'merge'
                });
              }
            })
        }
      });
  }
}
