/// <reference types="cypress" />

import HeaderTest from "./component/header";
import FooterTest from "./component/footer";
import AuthTest from "./component/auth";

describe("メニュー", () => {
  beforeEach(() => {
    cy.visit("/");
    AuthTest.login();
  });

  context("画面項目", () => {
    it("URL", () => {
      cy.url().should("eq", Cypress.config().baseUrl + "/Menu");
    });

    it("タイトル", () => {
      cy.title().should("eq", "メニュー");
    });

    it("ヘッダ", () => {
      HeaderTest.test();
    });

    it("テーブル", () => {
      cy.get(".theme_diary_title th")
        .first()
        .should("have.text", "No")
        .next()
        .should("have.text", "タイトル")
        .next()
        .should("have.text", "投稿日");
    });

    it("新規登録ボタン", () => {
      cy.get("#create").should("have.text", "新規登録");
    });

    it("フッタ", () => {
      FooterTest.test();
    });
  });

  HeaderTest.clickTest("/Menu");
});
