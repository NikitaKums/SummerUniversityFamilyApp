import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {PersonInRelationshipService} from "../services/person-in-relationship-service";
import {IPersonInRelationship} from "../interfaces/IPersonInRelationShip";
import { PersonsService } from "services/persons-service";

export var log = LogManager.getLogger('Person.Details');

@autoinject
export class Details {
  
  private personInRelationships: IPersonInRelationship;
  private firstAndLastName: string;
  private detailsForId: number;

  constructor(private router: Router,
    private personInRelationshipService: PersonInRelationshipService,
    private personService: PersonsService) {
    log.debug('constructor');
  }

  loadData() {
    log.debug("loadData");
    this.personService.fetchForRelationships(this.detailsForId).then(
      personInRelationship => {
      log.debug("personInRelationship", personInRelationship);
      this.personInRelationships = personInRelationship;
      this.firstAndLastName = this.personInRelationships.personData.firstName + " " + this.personInRelationships.personData.lastName;
    }
  );
  }

  deleteRelationship(idToDelete: number) {
    log.debug("deleteRelationship" + idToDelete);
    this.personInRelationshipService.delete(idToDelete).then(
      response => {
        if (response.status == 200){
          alert("Kustutatud");
          this.loadData();
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
    this.detailsForId = params.id;
    this.loadData();
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}