/// <reference types="cypress" />

context('Display Menu', () => {
  beforeEach(() => {
    cy.visit('/')
  })

  it('Create', () => {
    cy.get('#create').click()
    cy.get('#Title').type('Hello world')
    cy.get('#updFormControlTextarea1').type('Hello world')
    cy.get('#update').click()
    cy.get('#updModal').find('button').first().click()
  })
})
