import {PLATFORM, LogManager, autoinject} from "aurelia-framework";
import {RouterConfiguration, Router} from "aurelia-router";
import {AppConfig} from "./app-config";

export var log = LogManager.getLogger('MainRouter');

@autoinject
export class MainRouter {
  
  public router: Router;

  constructor(private appConfig: AppConfig) {
    log.debug('constructor');
  }

  configureRouter(config: RouterConfiguration, router: Router):void {
    log.debug('configureRouter');

    this.router = router;
    config.title = "Exam App - Aurelia";
    config.map(
      [
        {route: ['', 'home'], name: 'home', moduleId: PLATFORM.moduleName('home'), nav: false, title: 'Home'},

        {route: ['persons','persons/index'], name: 'persons' + 'Index', moduleId: PLATFORM.moduleName('persons/index'), nav: true, title: 'Nimekiri'},
        {route: 'persons/create', name: 'persons' + 'Create', moduleId: PLATFORM.moduleName('persons/create'), nav: false, title: 'Lisa'},
        {route: 'persons/edit/:id', name: 'persons' + 'Edit', moduleId: PLATFORM.moduleName('persons/edit'), nav: false, title: 'Muuda'},
        {route: 'persons/delete/:id', name: 'persons' + 'Delete', moduleId: PLATFORM.moduleName('persons/delete'), nav: false, title: 'Kustuta'},
        {route: 'persons/details/:id', name: 'persons' + 'Details', moduleId: PLATFORM.moduleName('persons/details'), nav: false, title: 'Lähivaade'},

        {route: 'personInRelationship/edit/:id', name: 'personInRelationship' + 'Edit', moduleId: PLATFORM.moduleName('personInRelationship/edit'), nav: false, title: 'Muuda'},

        {route: ['familyTree','familyTree/index'], name: 'familyTree' + 'Index', moduleId: PLATFORM.moduleName('familyTree/Index'), nav: true, title: 'Sugupuu'}
      ]
    );

  }

}