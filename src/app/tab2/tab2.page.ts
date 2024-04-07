import { Component, OnInit } from '@angular/core';
import { IonHeader, IonToolbar, IonTitle, IonContent, IonIcon, IonFab, IonFabButton, IonGrid, IonRow, IonCol, IonImg } from '@ionic/angular/standalone';
import { ExploreContainerComponent } from '../explore-container/explore-container.component';
import { ActionSheetController } from '@ionic/angular';
import { PhotoService, UserPhoto } from '../services/photo.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tab2',
  templateUrl: 'tab2.page.html',
  styleUrls: ['tab2.page.scss'],
  standalone: true,
  imports: [IonImg, IonCol, IonRow, IonGrid, IonFabButton, IonFab, IonIcon, IonHeader, IonToolbar, IonTitle, IonContent, ExploreContainerComponent, CommonModule ]
})
export class Tab2Page implements OnInit {

  constructor(public photoService: PhotoService,
    public actionSheetController: ActionSheetController
  ) {}

  async ngOnInit(): Promise<void> {
    await this.photoService.loadSaved();
  }
  addPhotoToGallery() {
    this.photoService.addNewToGallery();
  }

  public async showActionSheet(photo: UserPhoto, position: number) {
    const actionSheet = await this.actionSheetController.create({
      header: 'Photos',
      buttons: [{
        text: 'Delete',
        role: 'destructive',
        icon: 'trash',
        handler: () => {
          this.photoService.deletePicture(photo, position);
        }
      }, {
        text: 'Cancel',
        icon: 'close',
        role: 'cancel',
        handler: () => {
          // Nothing to do, action sheet is automatically closed
          }
      }]
    });
    await actionSheet.present();
  }
  

}
