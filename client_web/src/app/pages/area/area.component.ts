import { Component, OnInit } from '@angular/core';
import {AreaService} from './area.service';

@Component({
  selector: 'app-spotify',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.css']
})
export class AreaComponent implements OnInit {

  constructor(private spotifyService: AreaService) { }

  ngOnInit() {
  }
}
