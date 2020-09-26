export default class FooterTest {
  static test() {
    cy.get("footer div").should("contain.text", "© 2020 - Diary_Sample");
  }
}
