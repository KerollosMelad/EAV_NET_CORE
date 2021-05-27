# EAV_NET_CORE
Representing entities, attributes and values
for Dynamic attributes

The simplest implementation of EAV may have just three tables: entity, attribute and value. An example of this setup is shown here:
![image](https://user-images.githubusercontent.com/18659460/119754334-a065c100-bea0-11eb-81ea-d16f333309bd.png)

However, in such implementation we lose metadata information and all the values are stored as varchar, regardless of their data type. As a variation on this approach, we can alternatively implement a strongly-typed approach where a value of given data type is recorded in a table specific to that data type. An example schema is shown below and includes the metadata storage that we covered earlier.

![image](https://user-images.githubusercontent.com/18659460/119754350-a6f43880-bea0-11eb-83f1-636a6e21cca4.png)

the EAV model is in Magento https://magento.com/, the e-commerce platform.


