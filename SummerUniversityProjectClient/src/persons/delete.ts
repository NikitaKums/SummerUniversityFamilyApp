import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import { IPerson } from "interfaces/IPerson";
import { PersonsService } from "services/persons-service";

export var log = LogManager.getLogger('Persons.Delete');

@autoinject
export class Delete {

  private person: IPerson;

  constructor(private router: Router,
              private personsService: PersonsService) {
    log.debug('constructor');
  }

  // ============ View Methods ================
  submit() {
    log.debug('submit', this.person);

    this.personsService.delete(this.person.id).then(
      response => {
        if (response.status == 200){
          this.router.navigateToRoute("personsIndex");
        } else {
          log.debug("response", response.status);
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
    this.personsService.fetch(params.id).then(
      person => {
        log.debug("person", person);
        this.person = person;
      }
    );

    alert("Inimese kustutamine kustutab kõik temaga seotud sidemed.")
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}