import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IPerson} from "../interfaces/IPerson";
import {PersonsService} from "../services/persons-service";

export var log = LogManager.getLogger('Persons.Create');

@autoinject
export class Create {

  private person: IPerson;

  constructor(private router: Router,
              private personsService: PersonsService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit() {
    log.debug('person', this.person);

    this.person.age = Number(this.person.age);

    this.personsService.post(this.person).then(
      response => {
        if (response.status == 201){
          this.router.navigateToRoute("personsIndex");
        } else {
          alert("Palun sisestage õiged andmed");
          log.error('Error in response! ', response);
        }
      }
    );
  }

  // ============ View LifeCycle events ==============
  created(owningView: View, myView: View) {
    log.debug('created');
  }

  bind(bindingContext: Object, overrideContext: Object) {
    log.debug('bind');
  }

  attached() {
    log.debug('attached');
  }

  detached() {
    log.debug('detached');
  }

  unbind() {
    log.debug('unbind');
  }

  // ============= Router Events =============
  canActivate(params: any, routerConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    log.debug('canActivate');
  }

  activate(params: any, routerConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    log.debug('activate');
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}