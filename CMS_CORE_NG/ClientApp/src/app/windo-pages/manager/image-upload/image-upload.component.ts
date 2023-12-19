import { Component, OnInit } from '@angular/core';
import {  
  FileHandle  
} from '../image-drag.directive';
@Component({
  selector: 'app-image-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.css']
})
export class ImageUploadComponent implements OnInit {
  uploadedFiles: FileHandle[];  

  constructor() { }

  ngOnInit(): void {
  }
  filesDropped(files: FileHandle[]) {  
    this.uploadedFiles = files;  
    console.log("upload-files",this.uploadedFiles);
    
} 
}
