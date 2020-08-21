Feature: PDFFeatures
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@browser
Scenario: Download PDF
	Given I navigate to pdf url
	Then I can download pdf

@browser
Scenario: Verify PDF Text
	Given I navigate to pdf url
	When I download pdf
	Then I can read text from pdf


@browser
Scenario: Verify PDF contains Text
	Given I navigate to pdf url
	When I download pdf
	Then I can verify pdf contains required text

@browser
Scenario: Verify PDF page count
	Given I navigate to pdf url
	When I download pdf
	Then I can verify pdf page count