# MyRestAPI
Create a REST API function which receives transaction information from allowed Partner to further process the transaction information

Create a REST API function which receives transaction information from allowed Partner to further process the transaction information. This request will return a success / failed result with result messages back to allowed Partner.

Apply the validation against the fields in the request and return them in error code. Below is an example error result messages we are looking for. 

In continuation of the Question 1 API, you are tasked that calculates the final amount to be paid after applying several discount rules based on the totalamount. The business rules for applying discounts are as follows:

Base Discount:
If totalamount is less than MYR 200: No discount is applied.
If totalamount is between MYR 200 and MYR 500 (inclusive): Apply a 5% discount.
If totalamount is between MYR 501 and MYR 800 (inclusive): Apply a 7% discount.
If totalAmount is between MYR 801 and MYR 1200 (inclusive): Apply a 10% discount.
If totalAmount is greater than MYR 1200: Apply a 15% discount.

Conditional Discounts:
If totalAmount is a prime number above MYR 500: Apply an additional 8% discount.
If totalAmount ends in the digit 5 and is above MYR 900: Apply an additional 10% discount.

In continuation of the Basic scenarios, saved all the request body, response body and possible logs in text file to ease future troubleshooting using log4net.

Containerizing the application using Docker. 