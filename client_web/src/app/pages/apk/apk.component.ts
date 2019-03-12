import { Component, OnInit } from '@angular/core';
import {FileService} from '../../file.service';

@Component({
  selector: 'app-apk',
  templateUrl: './apk.component.html',
  styleUrls: ['./apk.component.css'],
  providers: [FileService]
})
export class ApkComponent implements OnInit {
  constructor (private fileService: FileService) {
  }

  ngOnInit() {
    this.fileService.downloadFile('app-release-unsigned.apk');
  }
}
