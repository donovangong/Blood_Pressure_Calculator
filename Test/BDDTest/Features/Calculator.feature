Feature: BP Calculation

  Scenario Outline: Use systolic and diastolic to calculate client's bloddpressure situation
    Given systolic is <Systolic> and diastolic is <Diastolic>
    When calculate pressure category 
    Then BP category is <Category>

    Examples:
      | Systolic | Diastolic | Category |
      | 70       | 40        | Low      |
      | 80       | 50        | Low      |
      | 89       | 59        | Low      |
      | 90       | 60        | Ideal    |
      | 110      | 75        | Ideal    |
      | 119      | 79        | Ideal    |
      | 120      | 80        | PreHigh  |
      | 130      | 85        | PreHigh  |
      | 139      | 89        | PreHigh  |
      | 140      | 90        | High     |
      | 150      | 95        | High     |
      | 190      | 100       | High     |

 Scenario: BP below minimum
  Given systolic is 50 and diastolic is 30
  When calculate pressure category
  Then BP category is invalid

Scenario: BP above maximum
  Given systolic is 200 and diastolic is 110
  When calculate pressure category
  Then BP category is invalid
