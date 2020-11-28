/// <reference types="cypress" />

import HeaderTest from "./component/header";
import FooterTest from "./component/footer";

describe("新規登録", () => {
  beforeEach(() => {
    cy.visit("/");
    cy.get("#create").click();
  });

  it("screenshot", () => {
    cy.screenshot("02_create");
  });

  context("画面項目", () => {
    it("URL", () => {
      cy.url().should("eq", Cypress.config().baseUrl + "/Create");
    });

    it("タイトル", () => {
      cy.title().should("eq", "新規登録");
    });

    it("ヘッダ", () => {
      HeaderTest.test();
    });

    it("日記-タイトル", () => {
      cy.get("#Title").should(
        "have.attr",
        "placeholder",
        "タイトルを入力してください"
      );
    });

    it("日記-本文", () => {
      cy.get("#Content").should(
        "have.attr",
        "placeholder",
        "本文を入力してください"
      );
    });

    it("登録ボタン", () => {
      cy.get("#create").should("have.text", "登録");
    });

    it("戻るボタン", () => {
      cy.get("#back").should("have.text", "戻る");
    });

    it("フッタ", () => {
      FooterTest.test();
    });
  });

  HeaderTest.clickTest("/Create");

  context("登録", () => {
    it("日記が登録される", () => {
      cy.get("#Title").type("Hello world");
      cy.get("#Content").type("Hello world");
      cy.get("#create").click();
      cy.get("#yes").click();

      cy.url().should("eq", Cypress.config().baseUrl + "/");
      const d = new Date();
      cy.get(".theme_diary_content td")
        .first()
        .should("have.text", "1")
        .next()
        .should("have.text", "Hello world")
        .next()
        .should(
          "have.text",
          `${d.getFullYear()}/${(d.getMonth() + 1)
            .toString()
            .padStart(2, "0")}/${d.getDate().toString().padStart(2, "0")}`
        );
    });

    it("タイトル必須エラー", () => {
      cy.get("#Content").type("Hello world");
      cy.get("#create").click();
      cy.get("#yes").click();

      cy.get(".field-validation-error").should(
        "have.text",
        "タイトルは必須です。"
      );
    });

    it("本文必須エラー", () => {
      cy.get("#Title").type("Hello world");
      cy.get("#create").click();
      cy.get("#yes").click();

      cy.get(".field-validation-error").should("have.text", "本文は必須です。");
    });
  });

  context("戻る", () => {
    it("メニューに戻る", () => {
      cy.get("#back").click();
      cy.url().should("eq", Cypress.config().baseUrl + "/");
    });
  });

  context("ダイアログ-いいえ", () => {
    it("登録されない", () => {
      cy.get("#Title").type("Hello world");
      cy.get("#Content").type("Hello world");
      cy.get("#create").click();
      cy.get("#no").click();
      cy.url().should("eq", Cypress.config().baseUrl + "/Create");
    });
  });
});
