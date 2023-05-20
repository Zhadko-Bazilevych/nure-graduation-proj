import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
//import { User } from 'src/app/models/user.model';
import { AuthenticationService } from 'src/app/services/auth.service';
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  orderby: string | undefined;
  params: any;
  constructor(private authenticationService : AuthenticationService, private route: ActivatedRoute, private router: Router) { }

  device: string | undefined | null;

  ngOnInit(): void {
    this.device = localStorage.getItem('device')
    if(this.device == null)
    {
      this.device = uuidv4();
      localStorage.setItem('device', this.device);
    }

    this.router.events
      .subscribe(e => {
        if (e.constructor.name === 'NavigationEnd' && this.router.navigated) {
          this.route.queryParams
            .subscribe(params => {
              if(params['code'] != null)
              {
                console.log('1')
                this.authenticationService.GetTokens(params['code'])
              }
            })
            .unsubscribe();
        }
      });
  }

  loginRedir()
  {
    this.authenticationService.RedirectLogin()
  }
}
