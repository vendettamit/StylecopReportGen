<xsl:stylesheet version="2.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xp="http://www.xmlprime.com/"
                xmlns:ext="urn:extensions">

  <!-- <xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="2.0"> -->
  <!-- <?xml version="1.0" encoding="UTF-8"?> -->
  <xsl:output method="html" />
  <xsl:param name="applicationPath" select="'.'" />

  <xsl:variable name="stylecop.root" select="StyleCopViolations" />
  <xsl:variable name="unique.source" select="$stylecop.root/Violation[not(@Source = preceding-sibling::Violation/@Source)]" />
  <xsl:key name="rules" match="/StyleCopViolations/Violation" use="@RuleId" />

  <xsl:template match="/">

    <div id="stylecop-report">
      <script language="javascript">
        function toggle (name, img)
        {
        var element = document.getElementById (name);

        if (element.style.display == 'none')
        element.style.display = '';
        else
        element.style.display = 'none';

        var img_element = document.getElementById (img);

        if (img_element.alt.indexOf ('minus.png') >= 0){
	img_element.src = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAwAAAAPCAMAAADnP957AAAABGdBTUEAAK/INwWK6QAAABl0RVh0U29mdHdhcmUAQWRvYmUgSW1hZ2VSZWFkeXHJZTwAAAAGUExURWlpaf///1gubPoAAAACdFJOU/8A5bcwSgAAAFZJREFUeNpiYEQCAAHEgMwBCCAQhwEGAAKIAUmWASCAYBwwBgggFA5AAEEYEA0MAAGEIgMQQCgcgABCMQ0ggFDsAQggFBcABBAKByCAUDgAAYTCAQgwADPcAIy9WFq3AAAAAElFTkSuQmCC';
	img_element.alt='plus.png';
        }
	else{
        img_element.src = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAwAAAAPCAMAAADnP957AAAABGdBTUEAAK/INwWK6QAAABl0RVh0U29mdHdhcmUAQWRvYmUgSW1hZ2VSZWFkeXHJZTwAAAAGUExURWlpaf///1gubPoAAAACdFJOU/8A5bcwSgAAAFRJREFUeNpiYEQCAAHEgMwBCCAQhwEGAAKIAUmWASCAUDgAAYTCAQggCAeigQEggFBkAAIIhQMQQCgcgABCsQcggFBcABBAKByAAELhAAQQCgcgwAA1kACQrCOu2AAAAABJRU5ErkJggg==';
	img_element.alt='minus.png';
	}
        }
      </script>
      <style type="text/css">
        #stylecop-report
        {
        font-family: Arial, Helvetica, sans-serif;
        margin-left: 0;
        margin-right: 0;
        margin-top: 0;
        }

        #stylecop-report .header
        {
        background-color: #566077;
        background-repeat: repeat-x;
        color: #fff;
        font-weight: bolder;
        height: 50px;
        vertical-align: middle;
        }

        #stylecop-report .headertext
        {
        height: 35px;
        margin-left: 15px;
        padding-top: 15px;
        width: auto;
        }

        #stylecop-report .wrapper
        {
        padding-left: 20px;
        padding-right: 20px;
        width: auto;
        }

        #stylecop-report .legend
        {
        background-color: #ffc;
        border: #d7ce28 1px solid;
        font-size: small;
        margin-top: 15px;
        padding: 5px;
        vertical-align: middle;
        width: inherit;
        }

        #stylecop-report .clickablerow
        {
        cursor: pointer;
        }

        #stylecop-report .tabletotal
        {
        border-top: 1px #000;
        font-weight: 700;
        }

        #stylecop-report .results-table
        {
        border-collapse: collapse;
        font-size: 12px;
        margin-top: 20px;
        text-align: left;
        width: 100%;
        }

        #stylecop-report .results-table th
        {
        background: #b9c9fe;
        border-bottom: 1px solid #fff;
        border-top: 4px solid #aabcfe;
        color: #039;
        font-size: 13px;
        font-weight: 400;
        padding: 8px;
        }

        #stylecop-report .results-table td
        {
        background: #e8edff;
        border-bottom: 1px solid #fff;
        border-top: 1px solid transparent;
        color: #669;
        padding: 5px;
        }

        #stylecop-report .errorlist td
        {
        background: #FFF;
        border-bottom: 0;
        border-top: 0 solid transparent;
        color: #000;
        padding: 0;
        }

        #stylecop-report .inner-results
        {
        border-collapse: collapse;
        font-size: 12px;
        margin-bottom: 3px;
        margin-top: 4px;
        text-align: left;
        /*width: 100%;*/
        width: auto;
        }

        #stylecop-report .inner-results td
        {
        background: #FFF;
        border-bottom: 1px solid #fff;
        border-top: 1px solid transparent;
        color: #669;
        padding: 3px;
        }

        .summaryline-Item
        {
        padding:10px;

        }
        .center{
        text-align:center;
        }
        
        .left{
        text-align:left
        }

        #stylecop-report .inner-header th
        {
        background: #b9c9fe;
        color: #039;
        }

        #stylecop-report .inner-rule-description
        {
        background-color: transparent;
        border-collapse: collapse;
        border: 0px;
        font-size: 12px;
        margin-bottom: 3px;
        margin-top: 4px;
        text-align: left;
        width: 100%;
        }

        #stylecop-report .inner-rule-description tr
        {
        background-color: transparent;
        border: 0px;
        }

        #stylecop-report .inner-rule-description td
        {
        background-color: transparent;
        border: 0px;
        }
      </style>
      <div class="header">
        <div class="headertext">
          <b>Your Project Name(Edit in stylecopviolations.xslt)</b> / StlyeCop 4.7.49: Code Analysis Report
          
          <div style="float:right" class="summaryline-Item">
            <small>
              <i>
                Dated:
                <xsl:variable name="currenttime" select="current-dateTime()"/>
                <xsl:value-of select="format-dateTime($currenttime, '[M01]/[D01]/[Y0001] at [H01]:[m01]:[s01]')" />
              </i>
            </small>
          </div>
          
        </div>
        
      </div>
      <div class="wrapper">

      </div>
      <div class="wrapper">
        <div class="legend">
          <div>
            Total Violations: <b><xsl:value-of select="count(//Violation)"/></b> <br/>
            <div class="results-table">
              Summary (By Categories):
              <table cellpadding="2" cellspacing="0" width="100%" class="inner-results">
                <thead>
                  <tr class="inner-header">
                    <th scope='col' class='left'>Category</th>
                    <th scope='col'>Violations</th>
                  </tr>
                </thead>
                <tbody>
                  <xsl:for-each-group select="/StyleCopViolations/Violation" group-by="@RuleNamespace">
                    <tr>
                      <td class="summaryline-Item">
                        <b><i>
                          <xsl:variable name="category" select="substring-after(@RuleNamespace, 'StyleCop.CSharp.')"/>
                          <xsl:if test="$category = ''">
                            <xsl:value-of select="@RuleNamespace"/>
                          </xsl:if>
                          <xsl:value-of select="$category" />
                          <!--<xsl:value-of select="substring-after(@RuleNamespace, 'StyleCop.CSharp.')"/>-->
                        </i>
                        </b>
                      </td>
                      <td class="summaryline-Item center">
                        <b>
                          <xsl:value-of select="count(current-group())"/>
                        </b>
                      </td>
                    </tr>
                  </xsl:for-each-group>
                </tbody>
              </table>
              <br/>
              Summary (By Rules):
              <table cellpadding="2" cellspacing="0" width="100%" class="inner-results">
                <thead>
                  <tr class="inner-header">
                    <th scope='col'>RuleId</th>
                    <th scope='col'>Description</th>
                    <th scope='col'>Violations</th>
                  </tr>
                </thead>
                <tbody>
                  <xsl:for-each-group select="/StyleCopViolations/Violation" group-by="@RuleId">
                    <tr>
                      <td class="summaryline-Item center">
                        <b>
                          <xsl:value-of select="current-grouping-key()"/>
                        </b>
                      </td>
                      <td class="summaryline-Item">
                        <i>
                          <xsl:value-of select="@Rule"/>
                        </i>
                      </td>
                      <td class="summaryline-Item center">
                        <b><xsl:value-of select="count(current-group())"/>
                        </b>
                      </td>
                    </tr>
                  </xsl:for-each-group>
                </tbody>
              </table>
            </div>
          </div>
        </div>
        <table class='results-table'>
          <thead>
            <tr>
              <th scope='col'></th>
              <th scope='col'></th>
              <th scope='col'>Source File</th>
              <th scope='col'>Violations</th>
            </tr>
          </thead>
          <tbody>
            <xsl:for-each select="$unique.source">
              <xsl:call-template name="print-module" />
            </xsl:for-each>
          </tbody>
        </table>
      </div>
    </div>
  </xsl:template>

  <xsl:template match ="summary-count-by-rule">
    <div>

    </div>
  </xsl:template>
  <xsl:template name="print-module">
    <xsl:variable name="module.id" select="generate-id()" />
    <xsl:variable name="source" select="./@Source"/>

    <tr class="clickablerow" onclick="toggle('{$module.id}', 'img-{$module.id}')">
      <td style="width: 10px">
        <!--<img id="img-{$module.id}" src="images/plus.png" />-->
        <img id="img-{$module.id}" 
	src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAwAAAAPCAMAAADnP957AAAABGdBTUEAAK/INwWK6QAAABl0RVh0U29mdHdhcmUAQWRvYmUgSW1hZ2VSZWFkeXHJZTwAAAAGUExURWlpaf///1gubPoAAAACdFJOU/8A5bcwSgAAAFZJREFUeNpiYEQCAAHEgMwBCCAQhwEGAAKIAUmWASCAYBwwBgggFA5AAEEYEA0MAAGEIgMQQCgcgABCMQ0ggFDsAQggFBcABBAKByCAUDgAAYTCAQgwADPcAIy9WFq3AAAAAElFTkSuQmCC" 
	alt="plus.png" />
      </td>
      <td style="width: 16px">
        <!--<img src="{$applicationPath}/images/error.png" />-->
        <!-- <img src="images/error.png" /> -->
<img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAABGdBTUEAAK/INwWK6QAAABl0RVh0U29mdHdhcmUAQWRvYmUgSW1hZ2VSZWFkeXHJZTwAAAJPSURBVDjLpZPLS5RhFMYfv9QJlelTQZwRb2OKlKuINuHGLlBEBEOLxAu46oL0F0QQFdWizUCrWnjBaDHgThCMoiKkhUONTqmjmDp2GZ0UnWbmfc/ztrC+GbM2dXbv4ZzfeQ7vefKMMfifyP89IbevNNCYdkN2kawkCZKfSPZTOGTf6Y/m1uflKlC3LvsNTWArr9BT2LAf+W73dn5jHclIBFZyfYWU3or7T4K7AJmbl/yG7EtX1BQXNTVCYgtgbAEAYHlqYHlrsTEVQWr63RZFuqsfDAcdQPrGRR/JF5nKGm9xUxMyr0YBAEXXHgIANq/3ADQobD2J9fAkNiMTMSFb9z8ambMAQER3JC1XttkYGGZXoyZEGyTHRuBuPgBTUu7VSnUAgAUAWutOV2MjZGkehgYUA6O5A0AlkAyRnotiX3MLlFKduYCqAtuGXpyH0XQmOj+TIURt51OzURTYZdBKV2UBSsOIcRp/TVTT4ewK6idECAihtUKOArWcjq/B8tQ6UkUR31+OYXP4sTOdisivrkMyHodWejlXwcC38Fvs8dY5xaIId89VlJy7ACpCNCFCuOp8+BJ6A631gANQSg1mVmOxxGQYRW2nHMha4B5WA3chsv22T5/B13AIicWZmNZ6cMchTXUe81Okzz54pLi0uQWp+TmkZqMwxsBV74Or3od4OISPr0e3SHa3PX0f3HXKofNH/UIG9pZ5PeUth+CyS2EMkEqs4fPEOBJLsyske48/+xD8oxcAYPzs4QaS7RR2kbLTTOTQieczfzfTv8QPldGvTGoF6/8AAAAASUVORK5CYII=" alt="error.png" />
      </td>
      <td>
        <xsl:value-of select="$source" />
      </td>
      <td>
        <xsl:value-of select="count(//Violation[@Source=$source])" />
      </td>
    </tr>
    <xsl:call-template name="print-module-error-list">
      <xsl:with-param name="module.id" select="$module.id"/>
      <xsl:with-param name="source" select="$source"/>
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="print-module-error-list">
    <xsl:param name="module.id" />
    <xsl:param name="source" />

    <tr id="{$module.id}" class="errorlist" style="display: none">
      <td></td>
      <td colspan="6">
        <table cellpadding="2" cellspacing="0" width="100%" class="inner-results">
          <thead>
            <tr class="inner-header">
              <th scope='col'></th>
              <th scope='col'>Line</th>
              <th scope='col'>Violation Message</th>
            </tr>
          </thead>
          <tbody>
            <xsl:for-each select='$stylecop.root/Violation[@Source = $source]'>
              <xsl:variable name="message.id" select="generate-id()" />
              <xsl:variable name="rule.id" select="@RuleId" />
              <xsl:variable name="rule.rule" select="@Rule" />
              <xsl:variable name="rule.namespace" select="@RuleNamespace" />
              <xsl:variable name="section" select="@Section" />

              <tr class="clickablerow" onclick="toggle('{$module.id}-{$message.id}', 'img-{$module.id}-{$message.id}')">
                <td style="width: 10px">
                  <img id="img-{$module.id}-{$message.id}" 
		  src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAwAAAAPCAMAAADnP957AAAABGdBTUEAAK/INwWK6QAAABl0RVh0U29mdHdhcmUAQWRvYmUgSW1hZ2VSZWFkeXHJZTwAAAAGUExURWlpaf///1gubPoAAAACdFJOU/8A5bcwSgAAAFZJREFUeNpiYEQCAAHEgMwBCCAQhwEGAAKIAUmWASCAYBwwBgggFA5AAEEYEA0MAAGEIgMQQCgcgABCMQ0ggFDsAQggFBcABBAKByCAUDgAAYTCAQgwADPcAIy9WFq3AAAAAElFTkSuQmCC" 
		  alt="plus.png" />
                </td>
                <td>
                  <xsl:value-of select="@LineNumber" />
                </td>
                <td>
                  <xsl:value-of select="text()" />
                </td>
              </tr>
              <tr id="{$module.id}-{$message.id}" style="display: none">
                <td></td>
                <td colspan="5">
                  <div class="legend">
                    <div>
                      <table cellpadding="2" cellspacing="0" width="100%" class="inner-rule-description">
                        <tr>
                          <td>
                            <b>Rule:</b>
                          </td>
                          <td>
                            <xsl:value-of select="$rule.rule" />
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <b>Rule Id:</b>
                          </td>
                          <td>
                            <xsl:value-of select="$rule.id" />
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <b>Rule Namespace:</b>
                          </td>
                          <td>
                            <xsl:value-of select="$rule.namespace" />
                          </td>
                        </tr>
                        <tr>
                          <td>
                            <b>Section:</b>
                          </td>
                          <td>
                            <!--<xsl:value-of select="$section" />-->
                            <!--Remove the ROOT. from the namespace-->
                            <xsl:value-of select="substring-after($section, 'Root.')" />
                          </td>
                        </tr>
                      </table>
                    </div>
                  </div>
                </td>
              </tr>
            </xsl:for-each>
          </tbody>
        </table>
      </td>
    </tr>
  </xsl:template>
</xsl:stylesheet>
