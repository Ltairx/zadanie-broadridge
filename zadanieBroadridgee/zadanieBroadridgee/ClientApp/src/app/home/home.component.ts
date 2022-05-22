import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public time: worldTime = {datetime: "", timezones: ""};

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<worldTime>(baseUrl + 'worldtime').subscribe(result => {
      this.time = result;
    }, error => console.error(error));
  }
}

interface worldTime{
  datetime: string
  timezones: string
}
