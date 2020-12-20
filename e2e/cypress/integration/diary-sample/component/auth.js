export default class AuthTest {

  static login() {
    cy.get("#Email").type("test@example.jp");
    cy.get("#Password").type("Password123!");
    cy.get("#Login").click();
  }

  static logout() {
    cy.get("#Logout").click();
  }
}
