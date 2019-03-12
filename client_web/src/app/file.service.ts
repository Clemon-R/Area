import { Injectable } from '@angular/core';

import {HttpClient} from '@angular/common/http';
import {FileSaverService} from 'ngx-filesaver';

@Injectable()
export class FileService {
  constructor(
    private http: HttpClient,
    private fileSaverService: FileSaverService
  ) {}

  downloadFile(fileId: string) {
    //const url = 'src/assets/' + fileId;
    const blob = new Blob(['yes monsieur'], { type: 'text/csv' });
    const url = window.URL.createObjectURL(blob);

    // Process the file downloaded
    this.http.get(url, {responseType: 'blob'}).subscribe(res => {
      this.fileSaverService.save((<any>res)._body, 'client.apk');
    });
  }
}
