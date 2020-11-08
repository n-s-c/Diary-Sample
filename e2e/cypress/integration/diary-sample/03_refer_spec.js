/// <reference types="cypress" />

import HeaderTest from "./component/header";
import FooterTest from "./component/footer";

describe("参照", () => {

  const baseUrl = Cypress.config().baseUrl;
  const inputTitle = "テストタイトル";
  const inputText = "テスト本文";
  var patternNo = 0;

  beforeEach(() => {
    cy.visit(baseUrl);
    cy.get("#create").click();
    cy.get("#Title").type(inputTitle);
    cy.get("#Content").type(inputText);
    cy.get("#create").click();
    cy.get("#yes").click();
    cy.get(".theme_diary_content td a").eq(0).click();
  });

  context("参照", () => {

    it("タイトル", () => {
      cy.title().should("eq", "参照");
      patternNo = 1;
    });

    it("ヘッダ", () => {
      HeaderTest.test();
      patternNo = 1;
    });

    it("参照画面", () => {
      cy.url().should("include", baseUrl + "Refer?id=");
      cy.get(".theme_diary_title h5 b").should("have.text", inputTitle);
      cy.get(".theme_diary_content").should("have.text", inputText);
      patternNo = 1;
    });
    
    it("編集ボタン押下", () => {
      cy.get("#edit").click();
      cy.url().should("include", baseUrl + "Edit?id=");
      patternNo = 2;
    });

    it("戻るボタン押下", () => {
      cy.get("#back").click();
      cy.url().should("eq", baseUrl);
      patternNo = 3;
    });

    it("フッタ", () => {
      FooterTest.test();
      patternNo = 1;
    });
  });

  afterEach(() => {
    if (patternNo == 2) {
      cy.get("#back").click();
    }
    if (patternNo == 3) {
      cy.get(".theme_diary_content td a").eq(0).click();
    }
    cy.get("#edit").click();
    cy.get("#delete").click();
    cy.get('[class="btn theme_warning"]').click();
  });

});
