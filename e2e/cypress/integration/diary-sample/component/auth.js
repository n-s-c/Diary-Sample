export default class AuthTest {
  static login() {
    cy.get("#Input_Email").type("test@example.jp");
    cy.get("#Input_Password").type("Password123!");
    cy.get("#Login").click();
  }

  static logout() {
    cy.get("#Logout").click();
  }
}
