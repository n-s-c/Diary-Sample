/// <reference types="cypress" />

import HeaderTest from "./component/header";
import FooterTest from "./component/footer";

describe("参照", () => {

  const baseUrl = Cypress.config().baseUrl + "/";
  const inputTitle = "テストタイトル";
  const inputText = "テスト本文";
  const editTitle = "を変更しました。";
  const edittText = "を変更しました。";
  var patternNo = 0;

  beforeEach(() => {
    cy.visit(baseUrl);
    cy.get("#create").click();
    cy.get("#Title").type(inputTitle);
    cy.get("#Content").type(inputText);
    cy.get("#create").click();
    cy.get("#yes").click();
    cy.get(".theme_diary_content td a").eq(0).click();
    cy.get("#edit").click();
  });

  context("編集", () => {

    it("タイトル", () => {
      cy.title().should("eq", "編集");
      patternNo = 1;
    });

    it("ヘッダ", () => {
      HeaderTest.test();
      patternNo = 1;
    });

    it("編集画面", () => {
      cy.url().should("include", baseUrl + "Edit?id=");
      cy.get("#title").should("have.value", inputTitle);
      cy.get("#content").should("have.value", inputText);
      patternNo = 1;
    });
    
    it("更新ボタン押下", () => {
      cy.get("#title").type(editTitle);
      cy.get("#content").type(edittText);
      cy.get("#update").click();
      cy.get('[class="btn theme_positive"]').click();
      cy.get(".theme_diary_title h5 b").should("have.text", inputTitle + editTitle);
      cy.get(".theme_diary_content").should("have.text", inputText + edittText);
      cy.url().should("include", baseUrl + "Refer?id=");
      patternNo = 2;
    });

    it("戻るボタン押下", () => {
      cy.get("#back").click();
      cy.url().should("include", baseUrl + "Refer?id=");
      patternNo = 2;
    });
    
    it("削除ボタン押下", () => {
      cy.get("#delete").click();
      cy.get('[class="btn theme_warning"]').click();
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
      cy.get("#edit").click();
    }
    
    if ((patternNo == 1) || (patternNo == 2)) {
      cy.get("#delete").click();
      cy.get('[class="btn theme_warning"]').click();
    }
  });

});
